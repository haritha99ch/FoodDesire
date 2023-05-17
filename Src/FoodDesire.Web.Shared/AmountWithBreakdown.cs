namespace FoodDesire.Web.Shared;
public class AmountWithBreakdown {
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; } = "USD";

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
