namespace FoodDesire.Models;
public sealed class Supply : TrackedEntity {
    [AllowNull]
    public int? SupplierId { get; set; }
    [Required]
    public int IngredientId { get; set; }
    [Required]
    public double Amount { get; set; } = 0;
    [AllowNull]
    public int? PaymentId { get; set; }
    [Required]
    public DateTime RequestedAt { get; set; } = DateTime.Now;
    [AllowNull]
    public DateTime? SuppliedDate { get; set; }
    [Required]
    public SupplyStatus Status { get; set; } = SupplyStatus.Pending;


    [ForeignKey(nameof(SupplierId))]
    public Supplier? Supplier { get; set; }
    [ForeignKey(nameof(IngredientId))]
    public Ingredient? Ingredient { get; set; }
    [ForeignKey(nameof(PaymentId))]
    public Payment? Payment { get; set; }
}

public enum SupplyStatus {
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Completed")]
    Completed,
    [Display(Name = "Accepted")]
    Accepted,
}
