using Flurl.Http;
using Microsoft.Identity.Client;

namespace FoodDesire.Core.Services;
public class AuthenticationService : IAuthenticationService {
    private readonly IAccountService _accountService;

    public AuthenticationService(IAccountService accountService) {
        _accountService = accountService;
    }

    private string _clientId { get; set; } = string.Empty;
    private IPublicClientApplication? _app;
    private readonly string[] scopes = new string[] {
        "user.read"
    };
    private AuthenticationResult? _result;

    public async Task<Account> AuthenticateUser(string clientId) {
        _clientId = clientId;
        _app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
        _result = await _app.AcquireTokenInteractive(scopes).ExecuteAsync();
        string userName = _result.Account.Username;
        Account? Account = await _accountService.GetAccountByEmail(userName);
        return Account;
    }

    public async Task<User> NewUser(string clientId) {
        _clientId = clientId;
        _app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
        _result = await _app.AcquireTokenInteractive(scopes).ExecuteAsync();
        dynamic json = await "https://graph.microsoft.com/beta/me/profile"
            .WithOAuthBearerToken(_result.AccessToken)
            .GetJsonAsync();
        var birthDay = (json.anniversaries as List<dynamic>)!.SingleOrDefault(e => (e.type as string)!.Equals("birthday"));
        var profilePicture = await "https://graph.microsoft.com/v1.0/me/photo/$value"
            .WithOAuthBearerToken(_result.AccessToken)
            .GetBytesAsync();
        var profilePictureB64 = Convert.ToBase64String(profilePicture);
        User user = new User() {
            FirstName = json.names[0].first,
            LastName = json.names[0].last,
            DateOfBirth = DateTime.Parse(birthDay!.date),
            Account = new Account() {
                Email = _result.Account.Username,
                ProfilePicture = profilePictureB64
            }
        };
        return user;
    }
}
