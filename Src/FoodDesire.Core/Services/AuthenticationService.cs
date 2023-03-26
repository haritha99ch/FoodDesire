using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace FoodDesire.Core.Services;
public class AuthenticationService : IAuthenticationService {
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;
    private readonly ICoreUserService _userService;

    private const string _defaultApplicationDataFolder = "FoodDesire.IMS/ApplicationData";
    private const string _defaultAuthenticationFile = "AuthenticationCache";
    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly StorageCreationProperties _storageProperties;

    private readonly IPublicClientApplication _app;
    private AuthenticationResult? _result;
    private dynamic? _profile;
    private readonly string _clientId;
    private readonly string _redirectUri = "http://localhost";
    private readonly string _profileUri = "https://graph.microsoft.com/beta/me/profile";
    private readonly string[] scopes = new string[] {
        "user.read",
        "profile",
    };

    public AuthenticationService(IAccountService accountService, ICoreUserService userService, IConfiguration configuration) {
        _accountService = accountService;
        _configuration = configuration;
        _userService = userService;
        _clientId = configuration["ClientId"]!;
        _storageProperties = new StorageCreationPropertiesBuilder(_defaultAuthenticationFile, $"{_localApplicationData}/{_defaultApplicationDataFolder}")
            .Build();

        _app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithRedirectUri(_redirectUri)
            .Build();
    }

    private async Task RegisterCache() {
        MsalCacheHelper? cacheHelper = await MsalCacheHelper.CreateAsync(_storageProperties);
        cacheHelper.RegisterCache(_app.UserTokenCache);
    }

    private async Task AuthenticateUser() {
        _result = await _app.AcquireTokenInteractive(scopes).ExecuteAsync();
    }

    private async Task<string> AcquireProfile(string accessToken) {
        try {
            _profile = await GetProfileJson(accessToken);
        } catch (FlurlHttpException) {
            try {
                await RegisterCache();
                var accounts = await _app.GetAccountsAsync();
                _result = await _app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
                accessToken = _result.AccessToken;
                _profile = await GetProfileJson(accessToken);
            } catch (Exception) {
                _profile = null;
            }
        } catch (Exception) {
            _profile = null;
        }
        return accessToken;
    }

    private async Task<dynamic> GetProfileJson(string accessToken) {
        return await _profileUri.WithOAuthBearerToken(accessToken).GetJsonAsync();
    }

    private async Task<User> AcquireUser(string accessToken) {
        if (_profile == null) await AcquireProfile(accessToken);
        dynamic? birthDay;
        try {
            birthDay = (_profile!.anniversaries as List<dynamic>)!.SingleOrDefault(e => (e.type as string)!.Equals("birthday"));
        } catch (Exception) {
            birthDay = null;
        }
        byte[]? profilePicture;
        try {
            profilePicture = await "https://graph.microsoft.com/v1.0/me/photo/$value"
                 .WithOAuthBearerToken(accessToken)
                 .GetBytesAsync();
        } catch (Exception) {
            profilePicture = null;
        }

        User user = new User() {
            FirstName = _profile!.names[0].first,
            LastName = _profile.names[0].last,
            DateOfBirth = birthDay?.date != null ? DateTime.Parse(birthDay.date) : null,
            Account = new Account() {
                Email = _profile.account[0].userPrincipalName,
                ProfilePicture = profilePicture
            }
        };
        return user;
    }

    public string AcquireAccessToken() {
        return _result?.AccessToken!;
    }

    public async Task<Account> AcquireAccount() {
        await RegisterCache();
        await AuthenticateUser();
        string userName = _result!.Account.Username;
        Account? account = await _accountService.GetAccountByEmail(userName);

        if (account == null) throw new Exception("Invalid Login");
        await UpdateUser(account.Id, _result!.AccessToken);
        return account;
    }

    public async Task<Account> AcquireAccount(string accessToken) {
        await RegisterCache();
        accessToken = await AcquireProfile(accessToken);
        if (_profile == null) return null!;

        var account = await _accountService.GetAccountByEmail(_profile!.account[0].userPrincipalName);
        if (account == null) return null!;

        await UpdateUser(account.Id, accessToken);
        return account;
    }

    public async Task<User> NewUser() {
        await AuthenticateUser();
        return await AcquireUser(_result!.AccessToken);
    }

    public async Task<User> UpdateUser(int userAccountId, string accessToken) {
        User currentUser = await _userService.GetUserByAccountId(userAccountId);
        User acquiredUser = await AcquireUser(accessToken);

        if (acquiredUser == null) return null!;
        bool hasUpdate = false;
        if (!string.Equals(acquiredUser.FirstName, currentUser.FirstName)) {
            currentUser.FirstName = acquiredUser.FirstName;
            hasUpdate = true;
        }
        if (!string.Equals(acquiredUser.LastName, currentUser.LastName)) {
            currentUser.LastName = acquiredUser.LastName;
            hasUpdate = true;
        }
        if (acquiredUser.DateOfBirth != null && currentUser.DateOfBirth != null) {
            if (acquiredUser.DateOfBirth != currentUser.DateOfBirth) {
                currentUser.DateOfBirth = currentUser.DateOfBirth;
                hasUpdate = true;
            }
        }
        if (!string.Equals(acquiredUser.Account!.Email, currentUser.Account!.Email)) {
            currentUser.Account.Email = acquiredUser.Account.Email;
            hasUpdate = true;
        }
        if (acquiredUser.Account!.ProfilePicture! != null) {
            if (acquiredUser.Account.ProfilePicture.Equals(currentUser.Account.ProfilePicture)) {
                currentUser.Account.ProfilePicture = acquiredUser.Account.ProfilePicture;
                hasUpdate = true;
            }
        }

        return !hasUpdate ? currentUser : await _userService.UpdateCoreUser(currentUser);
    }
}
