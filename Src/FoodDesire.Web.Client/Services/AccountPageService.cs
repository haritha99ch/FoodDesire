namespace FoodDesire.Web.Client.Services;
public class AccountPageService : AuthorizedService, IAccountPageService {

    public AccountPageService(HttpClient httpClient, IAuthenticationService authenticationService) : base(httpClient, authenticationService) {
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
        var response = await (await AddAuthorizationHeader()).GetAsync("/api/Account/Index");
        return response.StatusCode != HttpStatusCode.OK ? null : await response.Content.ReadFromJsonAsync<Customer>();
    }

    public async Task<string?> SignIn(SignIn signIn) {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("/api/Account/SignIn", signIn);
        if (response.StatusCode != HttpStatusCode.OK) return null;
        string token = await response.Content.ReadAsStringAsync();
        if (token == null) return token;
        await _authenticationService.SetAccessTokenAsync(token);
        return token;
    }

    public async Task<bool> SignOut() => await _authenticationService.UnsetAccessTokenAsync();

    public async Task<Customer?> SignUp(User user) {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("/api/Account/SignUp", user);
        return response.StatusCode != HttpStatusCode.OK ? null : await response.Content.ReadFromJsonAsync<Customer>();
    }
}
