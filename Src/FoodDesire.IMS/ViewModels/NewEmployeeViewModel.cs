namespace FoodDesire.IMS.ViewModels;
public partial class NewEmployeeViewModel : ObservableRecipient {
    [ObservableProperty]
    private UserDetail? _user;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPrimaryButtonEnabled))]
    private Role? _selectedRole;

    public List<Role> Roles => Enum.GetValues(typeof(Role)).Cast<Role>()
        .Where(e => e != Role.Admin && e != Role.Customer).ToList();
    public bool IsPrimaryButtonEnabled => SelectedRole != null;

    public NewEmployeeViewModel() {
        User = new();
    }
}
