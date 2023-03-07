namespace FoodDesire.IMS.Core.Models;
public class InventorySummary {
    public double TotalCapacity { get; set; }
    public double TotalCurrentQuantity { get; set; }
    public string AvailableSpace => $"\t{(TotalCurrentQuantity / TotalCapacity * 100):F2}%";
    public int LowInventoryCount { get; set; }
}
