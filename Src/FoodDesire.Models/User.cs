namespace FoodDesire.Models;
public class User : TrackedEntity {
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public DateTime DateOfBirth {
        get => _dateOfBirth;
        set {
            _dateOfBirth = value;
            Age = DateTime.Now.AddYears(-value.Year).Year;
        }
    }
    private DateTime _dateOfBirth { get; set; }
    [Required]
    public int Age { get; private set; } = 0;
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public int AddressId { get; set; }
    [Required]
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