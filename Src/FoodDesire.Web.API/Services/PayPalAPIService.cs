using System.Text;
using System.Text.Json;

namespace FoodDesire.Web.API.Services;
public class PayPalAPIService : IPayPalAPIService {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    private readonly string _clientId;
    private readonly string _clientSecret;
    private string _accessToken = string.Empty;

    public PayPalAPIService(HttpClient httpClient, IConfiguration configuration) {
        _httpClient = httpClient;
        _configuration = configuration;

        _clientId = _configuration["PayPal:ClientId"]!;
        _clientSecret = _configuration["PayPal:Secret"]!;
    }

    private async Task InitializeAccessToken() {
        HttpRequestMessage? request = new(HttpMethod.Post, "https://api-m.sandbox.paypal.com/v1/oauth2/token");
        request.Headers.Authorization = new(
            "Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}"))
        );
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
            { "grant_type", "client_credentials" }
        });

        HttpResponseMessage? response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) throw new Exception("Failed to retrieve access token from PayPal");
        TokenResponse? tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        _accessToken = tokenResponse!.AccessToken!;
    }

    public async Task<string> CreatePayPalOrder(Order order) {
        if (string.IsNullOrEmpty(_accessToken)) await InitializeAccessToken();
        decimal totalValue = order.Price;

        OrderRequest? orderRequest = new() {
            Intent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest> {
                new(){
                    AmountWithBreakdown = new(){
                        Value = totalValue.ToString("0.00")
                    },

                }
            }
        };

        HttpRequestMessage request = new(HttpMethod.Post, "https://api-m.sandbox.paypal.com/v2/checkout/orders");
        request.Headers.Authorization = new("Bearer", _accessToken);
        request.Content = new StringContent(
            JsonSerializer.Serialize(orderRequest, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage? response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) {
            string errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create PayPal order: {errorMessage}");
        }

        OrderResponse? orderResponse = await response.Content.ReadFromJsonAsync<OrderResponse>(
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );
        return orderResponse!.Id!;
    }
}
