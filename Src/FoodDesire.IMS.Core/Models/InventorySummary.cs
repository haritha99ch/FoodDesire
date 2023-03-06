namespace FoodDesire.IMS.Core.Models;
public class InventorySummary {
    public double TotalCapacity { get; set; }
    public double TotalCurrentQuantity { get; set; }
    public string AvailableSpace => $"{(TotalCurrentQuantity / TotalCapacity)}";
    public int LowInventoryCount { get; set; }
}
