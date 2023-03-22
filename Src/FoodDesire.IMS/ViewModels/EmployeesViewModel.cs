using AutoMapper;
using System.Collections.ObjectModel;

namespace FoodDesire.IMS.ViewModels;

public partial class EmployeesViewModel : ObservableRecipient, INavigationAware {
    private readonly IEmployeePageService _employeePageService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private UserDetail? _selected;
    public ObservableCollection<UserDetail> Users { get; private set; } = new();


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
        if (Selected != null) return;
        Selected = Users?.First();
    }
}
