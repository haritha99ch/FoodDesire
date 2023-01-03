namespace FoodDesire.Models;
public sealed class User: TrackedEntity {
    [Required, NotNull]
    public string? FirstName { get; set; }
    [Required, NotNull]
    public string? LastName { get; set; }
    [Required, NotNull]
    public DateTime DateOfBirth { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Required, NotNull]
    public int Age { get; set; }
    [Required, NotNull]
    public Gender Gender { get; set; }
    [Required, NotNull]
    public int AddressId { get; set; }
    [Required, NotNull]
    public int AccountId { get; set; }


    [ForeignKey(nameof(AddressId))]
    public Address? Address { get; set; }
    [ForeignKey(nameof(AccountId))]
    public Account? Account { get; set; }
}

public enum Gender {
    [Display(Name = "Male")]
    Male,
    [Display(Name = "Female")]
    Female
}