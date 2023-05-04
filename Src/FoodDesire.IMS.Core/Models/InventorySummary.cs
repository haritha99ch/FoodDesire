namespace FoodDesire.IMS.Core.Models;
public class InventorySummary {
    public double TotalCapacity { get; set; }
    public double TotalCurrentQuantity { get; set; }
    public double AvailableSpace => (100 - (TotalCurrentQuantity / TotalCapacity * 100));
    public int LowInventoryCount { get; set; }
}
