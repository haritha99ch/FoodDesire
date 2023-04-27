namespace FoodDesire.Web.Client.Services;
public class OrderPageService : AuthorizedService, IOrderPageService {
    public OrderPageService(HttpClient httpClient, IAuthenticationService authenticationService)
        : base(httpClient, authenticationService) { }

    public async Task<Order> GetAsync(int orderId) {
        HttpResponseMessage response = await (await AddAuthorizationHeader()).GetAsync($"/api/Order?{nameof(orderId)}={orderId}");
        return response.StatusCode != HttpStatusCode.OK ? null! : await response.Content.ReadFromJsonAsync<Order>() ?? null!;
    }

    public async Task<List<Order>> GetOrdersAsync() {
        HttpResponseMessage response = await (await AddAuthorizationHeader()).GetAsync($"/api/Order/Index");
        return response.StatusCode != HttpStatusCode.OK ? null! : await response.Content.ReadFromJsonAsync<List<Order>>() ?? null!;
    }
}
