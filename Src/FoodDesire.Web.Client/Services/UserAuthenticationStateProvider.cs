using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FoodDesire.Web.Client.Services;

public class UserAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly IAccountPageService _accountPageService;
    private readonly IAuthenticationService _authenticationService;

    private Customer? _customer;

    public UserAuthenticationStateProvider(IAccountPageService accountPageService, IAuthenticationService authenticationService) {
        _accountPageService = accountPageService;
        _authenticationService = authenticationService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        if (!await _authenticationService.IsAuthenticated()) return ReturnDefaultState();

        _customer = await _accountPageService.Get();
        if (_customer == null) return ReturnDefaultState();

        ClaimsIdentity? identity = new(new[]{
            new Claim(ClaimTypes.Name, _customer!.User!.FirstName!),
            new Claim(ClaimTypes.NameIdentifier, _customer.Id.ToString()),
        }, "Bearer");
        ClaimsPrincipal? user = new(identity);
        AuthenticationState? authState = new(user);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
        return authState;
    }

    private AuthenticationState ReturnDefaultState() {
        AuthenticationState? state = new(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }
}
