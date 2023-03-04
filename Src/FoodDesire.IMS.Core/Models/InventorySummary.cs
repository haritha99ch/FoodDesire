namespace FoodDesire.IMS.Core.Models;
public class InventorySummary {
    public double TotalCapacity { get; set; }
    public double TotalCurrentQuantity { get; set; }
    public double AvailableSpace { get; set; }
    public int LowInventoryCount { get; set; }
}
