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

    public async Task<Account> AuthenticateUser(string clientId) {
        _clientId = clientId;
        _app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
        AuthenticationResult? result = await _app.AcquireTokenInteractive(scopes).ExecuteAsync();
        string userName = result.Account.Username;
        Account? Account = await _accountService.GetAccountByEmail(userName);
        return Account;
    }
}
