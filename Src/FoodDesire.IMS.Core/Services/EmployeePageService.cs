namespace FoodDesire.IMS.Core.Services;
public class EmployeePageService : IEmployeePageService {
    private readonly IUserService<Chef> _chefUserService;
    private readonly IUserService<Supplier> _supplierUserService;
    private readonly IUserService<Deliverer> _delivererUserService;
    private readonly IAuthenticationService _authenticationService;

    public EmployeePageService(
        IUserService<Chef> chefUserService,
        IUserService<Supplier> supplierUserService,
        IUserService<Deliverer> delivererUserService,
        IAuthenticationService authenticationService
        ) {
        _chefUserService = chefUserService;
        _supplierUserService = supplierUserService;
        _delivererUserService = delivererUserService;
        _authenticationService = authenticationService;
    }

    public async Task<Chef> NewChef() {
        User user = await _authenticationService.NewUser();
        user.Account!.Role = Role.Chef;
        Chef chef = new Chef() {
            User = user,
        };
        chef = await _chefUserService.CreateAccount(chef);
        return chef;
    }

    public Task<Deliverer> NewDeliverer(Deliverer deliverer) {
        throw new NotImplementedException();
    }

    public Task<Supplier> NewSupplier(Supplier supplier) {
        throw new NotImplementedException();
    }
}
