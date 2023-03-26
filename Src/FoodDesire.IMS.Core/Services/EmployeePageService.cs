using Microsoft.Extensions.DependencyInjection;

namespace FoodDesire.IMS.Core.Services;
public class EmployeePageService : IEmployeePageService {
    private readonly IUserService<Chef> _chefUserService;
    private readonly IUserService<Supplier> _supplierUserService;
    private readonly IUserService<Deliverer> _delivererUserService;
    private readonly ICoreUserService _coreUserService;
    private readonly IPaymentService _paymentService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthenticationService _authenticationService;

    public EmployeePageService(
        IUserService<Chef> chefUserService,
        IUserService<Supplier> supplierUserService,
        IUserService<Deliverer> delivererUserService,
        ICoreUserService coreUserService,
        IAuthenticationService authenticationService,
        IPaymentService paymentService,
        IServiceProvider serviceProvider
        ) {
        _chefUserService = chefUserService;
        _supplierUserService = supplierUserService;
        _delivererUserService = delivererUserService;
        _authenticationService = authenticationService;
        _coreUserService = coreUserService;
        _paymentService = paymentService;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<Chef>> GetAllChef() => await _chefUserService.GetAll();

    public async Task<List<Deliverer>> GetAllDeliverer() => await _delivererUserService.GetAll();

    public async Task<List<Supplier>> GetAllSupplier() => await _supplierUserService.GetAll();

    public async Task<Chef> GetChef(int chefId) => await _chefUserService.GetById(chefId);

    public async Task<Deliverer> GetDeliverer(int delivererId) => await _delivererUserService.GetById(delivererId);

    public async Task<Supplier> GetSupplier(int supplierId) => await _supplierUserService.GetById(supplierId);

    public async Task<Payment> MakePaymentForEmployee(Payment payment) => await _paymentService.SalaryForEmployee(payment);

    public async Task<List<User>> GetAllEmployees() => await _coreUserService.GetAllEmployees();

    private async Task<User> GetNewUser() {
        return await _authenticationService.NewUser();
    }

    public async Task<T> NewUser<T>() where T : BaseUser, new() {
        User user = await GetNewUser();
        if (typeof(T) == typeof(Chef)) user.Account!.Role = Role.Chef;
        if (typeof(T) == typeof(Supplier)) user.Account!.Role = Role.Supplier;
        if (typeof(T) == typeof(Deliverer)) user.Account!.Role = Role.Deliverer;
        if (typeof(T) == typeof(Admin)) user.Account!.Role = Role.Admin;

        T employee = new() { User = user };
        employee = await _serviceProvider.GetService<IUserService<T>>()!
            .CreateAccount(employee);
        return employee;
    }
}
