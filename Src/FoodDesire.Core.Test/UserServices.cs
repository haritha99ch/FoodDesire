using FoodDesire.Models;

namespace FoodDesire.Core.Test;
[TestFixture]
public class UserServices {
    private IAdminService adminService;
    private IRepository<Admin> _adminRepository;
    private ITrackingRepository<User> _userTRepository;
    private protected FoodDesireContext _context;

    [OneTimeSetUp]
    public void Setup() {
        _context = DbContextHelper.GetContext("UserServices");
        if(!_context.Database.EnsureCreated()) return;
        _adminRepository = new Repository<Admin>(_context);
        _userTRepository = new TrackingRepository<User>(_context);
        adminService = new AdminService(_context, _adminRepository, _userTRepository);
    }

    [OneTimeTearDown]
    public void TearDown() {
        //_context.Database.EnsureDeleted();
    }
    [Test, Order(1)]
    public async Task CreateAnAdmin() {
        Admin admin = new Admin() {
            User = new User() {
                FirstName = "Admin",
                LastName = "Nimda",
                DateOfBirth = new DateTime(1999, 5, 2),
                Account = new Account() {
                    Email = "admin@fooddesire.com",
                    Password = "1234",
                },
                Address = new Address() {
                    No = "2",
                    Street1 = "Street1",
                    Street2 = "Street2",
                    City = "Diyatalawa",
                    PostalCode = 1290
                },
                Gender = Gender.Male,
            },
        };
        Admin saveAdmin = await adminService.CreateAccount(admin);
        Assert.That(admin.User.FirstName, Is.EqualTo(saveAdmin.User!.FirstName));
    }
    [Test, Order(2)]
    public async Task LoginAsAdmin() {
        Admin admin = await adminService.GetByEmailAndPassword("admin@fooddesire.com", "1234");
        Assert.IsNotNull(admin);
    }
    [Test, Order(3)]
    public async Task UpdateAdmin() {
        Admin admin = await adminService.GetByIdPopulated(1);

        admin.User!.FirstName = "AdminUpdated";
        await adminService.UpdateAccount(admin);
        Admin updateAdmin = await adminService.GetByIdPopulated(1);
        Assert.That(admin.User.FirstName, Is.EqualTo(updateAdmin.User!.FirstName));
    }
}
