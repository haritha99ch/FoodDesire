namespace FoodDesire.Web.Client.Services;
public class AccountPageService : IAccountPageService {
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationService _authenticationService;

    public AccountPageService(HttpClient httpClient, IAuthenticationService authenticationService) {
        _httpClient = httpClient;
        _authenticationService = authenticationService;
    }

    public async Task<bool> Delete() {
        HttpResponseMessage? response = await (await AddAuthorizationHeader()).DeleteAsync("/api/Account/Edit");
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<Customer?> Edit(Customer customer) {
        HttpResponseMessage? response = await (await AddAuthorizationHeader()).PatchAsJsonAsync("/api/Account/Edit", customer);
        return await response.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task<Customer?> Get() {
        Customer? response = await (await AddAuthorizationHeader()).GetFromJsonAsync<Customer>("/api/Account/Index");
        return response;
    }

    public async Task<string?> SignIn(SignIn signIn) {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("/api/Account/SignIn", signIn);
        string? token = await response.Content.ReadFromJsonAsync<string>();
        if (token == null) return token;
        await _authenticationService.SetAccessTokenAsync(token);
        return token;
    }

    public async Task<bool> SignOut() => await _authenticationService.UnsetAccessTokenAsync();

    public async Task<Customer?> SignUp(User user) {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("/api/Account/SignUp", user);
        return await response.Content.ReadFromJsonAsync<Customer>();
    }

    private async Task<HttpClient> AddAuthorizationHeader() {
        var token = await _authenticationService.GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(token)) {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return _httpClient;
    }
}
