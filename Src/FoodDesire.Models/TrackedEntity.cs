namespace FoodDesire.Models;
public abstract class TrackedEntity: Entity {
    [Required, NotNull]
    public bool Deleted { get; set; } = false;
}
