namespace FoodDesire.Web.Client.Services;

public abstract class AuthorizedService {
    protected readonly HttpClient _httpClient;
    protected readonly IAuthenticationService _authenticationService;
    protected AuthorizedService(HttpClient httpClient, IAuthenticationService authenticationService) {
        _httpClient = httpClient;
        _authenticationService = authenticationService;
    }
    protected async Task<HttpClient> AddAuthorizationHeader() {
        var token = await _authenticationService.GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(token)) {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return _httpClient;
    }
}
