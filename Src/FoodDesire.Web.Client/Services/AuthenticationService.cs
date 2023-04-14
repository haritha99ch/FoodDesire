namespace FoodDesire.Web.Client.Services;
public class AuthenticationService : IAuthenticationService {
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(ILocalStorageService localStorage) {
        _localStorage = localStorage;
    }

    public async Task<string> GetAccessTokenAsync() => await _localStorage.GetItemAsync<string>("authToken");

    public async Task<bool> IsAuthenticated() {
        string token = await GetAccessTokenAsync();
        return token != null;
    }

    public async Task SetAccessTokenAsync(string token) => await _localStorage.SetItemAsync("authToken", token);

    public async Task<bool> UnsetAccessTokenAsync() {
        await SetAccessTokenAsync(null!);
        return !await IsAuthenticated();
    }
}
