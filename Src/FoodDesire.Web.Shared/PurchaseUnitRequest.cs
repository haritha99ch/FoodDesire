namespace FoodDesire.Web.Shared;
public class PurchaseUnitRequest {
    [JsonPropertyName("amount")]
    public AmountWithBreakdown? AmountWithBreakdown { get; set; }
}
