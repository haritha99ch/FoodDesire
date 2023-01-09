namespace FoodDesire.Models;
public sealed class Order: TrackedEntity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    private ICollection<FoodItem>? _foodItems { get; set; }
    public ICollection<FoodItem>? FoodItems {
        get => _foodItems;
        set {
            _foodItems = value;
            if(Status != OrderStatus.Preparing) return;
            value!.ToList().ForEach(e => {
                if(e.Status != FoodItemStatus.Prepared) {
                    Status = OrderStatus.Preparing;
                    return;
                }
                Status = OrderStatus.Prepared;
            });
        }
    }


    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    public Delivery? Delivery { get; set; }
}

public enum OrderStatus {
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Ordered")]
    Ordered,
    [Display(Name = "Preparing")]
    Preparing,
    [Display(Name = "Prepared")]
    Prepared,
    [Display(Name = "Delivering")]
    Delivering,
    [Display(Name = "Delivered")]
    Delivered
}
