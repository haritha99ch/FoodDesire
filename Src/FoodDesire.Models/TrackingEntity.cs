namespace FoodDesire.Models;
public abstract class TrackingEntity: Entity {
    [Required, NotNull]
    public bool Deleted { get; set; } = false;
}
