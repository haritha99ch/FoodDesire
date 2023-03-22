namespace FoodDesire.IMS.Core.Services;
public class EmployeePageService : IEmployeePageService {
    private readonly IUserService<Chef> _chefUserService;
    private readonly IUserService<Supplier> _supplierUserService;
    private readonly IUserService<Deliverer> _delivererUserService;
    private readonly IUserService<Admin> _adminUserService;
    private readonly ICoreUserService _coreUserService;
    private readonly IPaymentService _paymentService;
    private readonly IAuthenticationService _authenticationService;

    public EmployeePageService(
        IUserService<Chef> chefUserService,
        IUserService<Supplier> supplierUserService,
        IUserService<Deliverer> delivererUserService,
        ICoreUserService coreUserService,
        IAuthenticationService authenticationService,
        IPaymentService paymentService,
        IUserService<Admin> adminUserService
        ) {
        _chefUserService = chefUserService;
        _supplierUserService = supplierUserService;
        _delivererUserService = delivererUserService;
        _authenticationService = authenticationService;
        _coreUserService = coreUserService;
        _paymentService = paymentService;
        _adminUserService = adminUserService;
    }

    public async Task<List<Chef>> GetAllChef() => await _chefUserService.GetAll();

    public async Task<List<Deliverer>> GetAllDeliverer() => await _delivererUserService.GetAll();

    public async Task<List<Supplier>> GetAllSupplier() => await _supplierUserService.GetAll();

    public async Task<Chef> GetChef(int chefId) => await _chefUserService.GetById(chefId);

    public async Task<Deliverer> GetDeliverer(int delivererId) => await _delivererUserService.GetById(delivererId);

    public async Task<Supplier> GetSupplier(int supplierId) => await _supplierUserService.GetById(supplierId);

    public async Task<Payment> MakePaymentForEmployee(Payment payment) => await _paymentService.SalaryForEmployee(payment);

    public async Task<List<User>> GetAllEmployees() => await _coreUserService.GetAllEmployees();

    public async Task<Chef> NewChef() {
        User user = await GetNewUser();
        user.Account!.Role = Role.Chef;
        Chef chef = new Chef() {
            User = user,
        };
        chef = await _chefUserService.CreateAccount(chef);
        return chef;
    }

    public async Task<Deliverer> NewDeliverer(VehicleType vehicleType, string licenseNo) {
        User user = await GetNewUser();
        user.Account!.Role = Role.Chef;
        Deliverer deliverer = new Deliverer() {
            User = user,
            LicenseNo = licenseNo,
            VehicleType = vehicleType
        };
        deliverer = await _delivererUserService.CreateAccount(deliverer);
        return deliverer;
    }

    public async Task<Supplier> NewSupplier() {
        User user = await GetNewUser();
        user.Account!.Role = Role.Chef;
        Supplier supplier = new Supplier() {
            User = user,
        };
        supplier = await _supplierUserService.CreateAccount(supplier);
        return supplier;
    }

    public async Task<Admin> NewAdmin() {
        User user = await GetNewUser();
        user.Account!.Role = Role.Admin;
        Admin admin = new Admin() {
            User = user
        };
        admin = await _adminUserService.CreateAccount(admin);
        return admin;
    }

    private async Task<User> GetNewUser() {
        return await _authenticationService.NewUser();
    }
}
