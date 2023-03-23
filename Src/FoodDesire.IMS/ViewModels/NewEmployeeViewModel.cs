namespace FoodDesire.IMS.ViewModels;
public partial class NewEmployeeViewModel : ObservableObject {
    public UserDetail User { get; set; }
    public List<Role> Roles => Enum.GetValues(typeof(Role)).Cast<Role>()
        .Where(e => e != Role.Admin && e != Role.Customer).ToList();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPrimaryButtonEnabled))]
    private Role? _selectedRole;
    public bool IsPrimaryButtonEnabled => SelectedRole != null;

    public NewEmployeeViewModel() {
        User = new();
    }
}
