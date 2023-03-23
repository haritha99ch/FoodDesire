namespace FoodDesire.Models;
public class User : TrackedEntity {
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [AllowNull]
    public DateTime? DateOfBirth {
        get => _dateOfBirth;
        set {
            _dateOfBirth = value;
            if (value == null) return;
            Age = DateTime.Now.AddYears(-value.Value.Year).Year;
        }
    }
    private DateTime? _dateOfBirth { get; set; }
    [AllowNull]
    public int? Age { get; private set; }
    [AllowNull]
    public Gender? Gender { get; set; }
    [AllowNull]
    [MaxLength(10), MinLength(10)]
    public int PhoneNumber { get; set; } = 0705924764;
    [AllowNull]
    public int? AddressId { get; set; }
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
