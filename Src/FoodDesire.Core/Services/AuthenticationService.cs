using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace FoodDesire.Core.Services;
public class AuthenticationService : IAuthenticationService {
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;
    private readonly ICoreUserService _userService;

    private dynamic? _profile;
    private readonly IPublicClientApplication _app;
    private readonly string _clientId;
    private readonly string[] scopes = new string[] {
        "user.read"
    };
    private AuthenticationResult? _result;

    public AuthenticationService(IAccountService accountService, ICoreUserService userService, IConfiguration configuration) {
        _accountService = accountService;
        _configuration = configuration;
        _userService = userService;
        _clientId = configuration["ClientId"]!;
        _app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
    }

    private async Task AuthenticateUser() {
        _result = await _app.AcquireTokenInteractive(scopes).ExecuteAsync();
    }

    private async Task AcquireProfile(string accessToken) {
        try {
            _profile = await "https://graph.microsoft.com/beta/me/profile"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync();
        } catch (Exception) {
            _profile = null;
        }
    }

    private async Task<User> AcquireUser(string accessToken) {
        await AcquireProfile(accessToken);
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
            FirstName = _profile.names[0].first,
            LastName = _profile.names[0].last,
            DateOfBirth = DateTime.Parse(birthDay!.date),
            Account = new Account() {
                Email = _profile.account[0].userPrincipalName,
                ProfilePicture = profilePicture
            }
        };
        return user;
    }

    public string AcquireAccessToken() {
        return _result!.AccessToken;
    }

    public async Task<Account> AcquireAccount() {
        await AuthenticateUser();
        string userName = _result!.Account.Username;
        Account? account = await _accountService.GetAccountByEmail(userName);

        if (account == null) throw new Exception("Invalid Login");
        await UpdateUser(account.Id, _result!.AccessToken);
        return account;
    }

    public async Task<Account> AcquireAccount(string accessToken) {
        await AcquireProfile(accessToken);
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
