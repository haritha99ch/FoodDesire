namespace FoodDesire.Web.Shared;
public class OrderResponse {
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
