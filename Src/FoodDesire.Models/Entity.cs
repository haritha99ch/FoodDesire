namespace FoodDesire.Models;
public abstract class Entity {
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }
}
