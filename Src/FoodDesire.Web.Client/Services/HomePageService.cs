namespace FoodDesire.Web.Client.Services;
public class HomePageService : AuthorizedService, IHomePageService {
    public HomePageService(HttpClient httpClient, IAuthenticationService authenticationService)
        : base(httpClient, authenticationService) { }

    public async Task<List<Recipe>> GetTop10Recipes() {
        HttpResponseMessage? response = await (await AddAuthorizationHeader()).GetAsync("/api/Home/Index");
        return response.StatusCode != HttpStatusCode.OK ? null! : await response.Content.ReadFromJsonAsync<List<Recipe>>() ?? new();
    }
}
