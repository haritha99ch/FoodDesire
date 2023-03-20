namespace FoodDesire.IMS.ViewModels;

public class EmployeesViewModel : ObservableRecipient {
    private readonly IEmployeePageService _employeePageService;
    public EmployeesViewModel(IEmployeePageService employeePageService) {
        _employeePageService = employeePageService;
        OnInit();
    }

    public async void OnInit() {
        Chef user = await _employeePageService.NewChef();
    }
}
