namespace FoodDesire.IMS.Core.Models;
public class IngredientDetail {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? IngredientCategory { get; set; }
    public double CurrentQuantity { get; set; }
    public decimal CurrentPricePerUnit { get; set; }
    public double MaximumQuantity { get; set; }
    public string DisplayMaximumQuantity
        => MaximumQuantity >= 1000 ? $"{(MaximumQuantity / 1000):F2} K{Measurement}" : $"{MaximumQuantity} {Measurement}";
    public string DisplayCurrentQuantity
        => CurrentQuantity >= 1000 ? $"{(CurrentQuantity / 1000):F2} K{Measurement}" : $"{CurrentQuantity} {Measurement}";
    public Measurement Measurement { get; set; }
    public string DisplayMeasurement => Measurement switch {
        Measurement.Grams => "g",
        Measurement.Liquid => "ml",
        Measurement.Each => "each",
        _ => "",
    };
    public int AvailableSpacePerCent => (int)(CurrentQuantity / MaximumQuantity * 100);
    public bool IsLowInventory => CurrentQuantity / MaximumQuantity < 0.2;
    public bool IsMediumInventory => CurrentQuantity / MaximumQuantity > 0.2 && CurrentQuantity / MaximumQuantity < 0.5;
}
