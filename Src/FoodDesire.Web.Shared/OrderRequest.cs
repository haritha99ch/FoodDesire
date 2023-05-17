namespace FoodDesire.Web.Shared;
public class OrderRequest {
    [JsonPropertyName("intent")]
    public string? Intent { get; set; }

    [JsonPropertyName("purchase_units")]
    public List<PurchaseUnitRequest> PurchaseUnits { get; set; } = new();
}
