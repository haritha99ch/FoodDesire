namespace FoodDesire.Models;
public sealed class Supply : TrackedEntity {
    [Required]
    public int SupplierId { get; set; }
    [Required]
    public int IngredientId { get; set; }
    [Required]
    public double Amount { get; set; } = 0;
    public DateTime SuppliedDate { get; set; } = DateTime.Now;


    [ForeignKey(nameof(SupplierId))]
    public Supplier? Supplier { get; set; }
    [ForeignKey(nameof(IngredientId))]
    public Ingredient? Ingredient { get; set; }
}
