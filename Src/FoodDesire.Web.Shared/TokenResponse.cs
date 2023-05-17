namespace FoodDesire.Web.Shared;
public class TokenResponse {
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}
