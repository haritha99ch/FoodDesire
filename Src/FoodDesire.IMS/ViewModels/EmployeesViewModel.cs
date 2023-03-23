namespace FoodDesire.IMS.ViewModels;
public partial class EmployeesViewModel : ObservableRecipient, INavigationAware {
    private readonly IEmployeePageService _employeePageService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private UserDetail? _selected;
    public ObservableCollection<UserDetail> Users { get; private set; } = new();
    [ObservableProperty]
    private bool _isUserAdding = false;


    public EmployeesViewModel(IEmployeePageService employeePageService, IMapper mapper) {
        _employeePageService = employeePageService;
        _mapper = mapper;
    }

    public async void OnNavigatedTo(object parameter) {
        List<User> users = await _employeePageService.GetAllEmployees();
        users.ForEach(e => Users.Add(_mapper.Map<UserDetail>(e)));
    }

    public void OnNavigatedFrom() {
    }

    public void EnsureItemSelected() {
        if (!Users.Any()) return;
        if (Selected != null) return;
        Selected = Users?.First();
    }

    public async Task AddNewUser<T>() where T : BaseUser, new() {
        T employee = await _employeePageService.NewUser<T>();
        Users.Insert(0, _mapper.Map<UserDetail>(employee.User));
    }
}
