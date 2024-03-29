namespace FoodDesire.Core.Test;
[TestFixture]
public class UserServices : Services {
    public UserServices() : base("UserServices") { }

    [OneTimeSetUp]
    public async Task SetUp() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task CreateAdmin() {
        Admin admin = UserDataHelper.GetAdminPayload();
        Admin saveAdmin = await _adminService.CreateAccount(admin);
        Assert.That(admin.User!.FirstName, Is.EqualTo(saveAdmin.User!.FirstName));
    }

    [Test, Order(2)]
    public async Task LoginAsAdmin() {
        Admin admin = await _adminService.GetByEmailAndPassword("admin@fooddesire.com", "1234");
        Assert.IsNotNull(admin);
    }

    [Test, Order(3)]
    public async Task UpdateAdmin() {
        Admin admin = await _adminService.GetById(1);
        admin.User!.FirstName = "AdminUpdated";
        await _adminService.UpdateAccount(admin);
        Admin updateAdmin = await _adminService.GetById(1);
        Assert.That(admin.User.FirstName, Is.EqualTo(updateAdmin.User!.FirstName));
    }

    [Test, Order(4)]
    public async Task CreateSupplier() {
        Supplier supplier = UserDataHelper.GetSupplierPayload();
        Supplier saveSupplier = await _supplierService.CreateAccount(supplier);
        Assert.That(supplier!.User!.FirstName, Is.EqualTo(saveSupplier!.User!.FirstName));
    }

    [Test, Order(5)]
    public async Task LoginAsSupplier() {
        Supplier supplier = await _supplierService.GetByEmailAndPassword("supplier@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }

    [Test, Order(6)]
    public async Task UpdateSupplier() {
        Supplier supplier = await _supplierService.GetById(1);
        supplier!.User!.FirstName = "SupplierUpdated";
        await _supplierService.UpdateAccount(supplier);
        Supplier updateAdmin = await _supplierService.GetById(1);
        Assert.That(supplier!.User!.FirstName, Is.EqualTo(updateAdmin!.User!.FirstName));
    }

    [Test, Order(7)]
    public async Task CreateChef() {
        Chef chef = UserDataHelper.GetChefPayload();
        Chef saveChef = await _chefService.CreateAccount(chef);
        Assert.That(chef!.User!.FirstName, Is.EqualTo(saveChef!.User!.FirstName));
    }

    [Test, Order(8)]
    public async Task LoginAsChef() {
        Chef supplier = await _chefService.GetByEmailAndPassword("chef@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }

    [Test, Order(9)]
    public async Task UpdateChef() {
        Chef chef = await _chefService.GetById(1);
        chef!.User!.FirstName = "ChefUpdated";
        await _chefService.UpdateAccount(chef);
        Chef updateSupplier = await _chefService.GetById(1);
        Assert.That(chef!.User!.FirstName, Is.EqualTo(updateSupplier!.User!.FirstName));
    }

    [Test, Order(10)]
    public async Task CreateDeliverer() {
        Deliverer deliverer = new() {
            User = new User() {
                FirstName = "Deliverer",
                LastName = "Rereviled",
                DateOfBirth = new DateTime(1999, 5, 2),
                Account = new Account() {
                    Email = "deliverer@fooddesire.com",
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
            LicenseNo = "2",
        };
        Deliverer saveDeliverer = await _delivererService.CreateAccount(deliverer);
        Assert.That(deliverer.User!.FirstName, Is.EqualTo(saveDeliverer!.User!.FirstName));
    }

    [Test, Order(11)]
    public async Task LoginAsDeliverer() {
        Deliverer deliverer = await _delivererService.GetByEmailAndPassword("deliverer@fooddesire.com", "1234");
        Assert.IsNotNull(deliverer);
    }

    [Test, Order(12)]
    public async Task UpdateDeliverer() {
        Deliverer deliverer = await _delivererService.GetById(1);
        deliverer!.User!.FirstName = "DelivererUpdated";
        await _delivererService.UpdateAccount(deliverer);
        Deliverer updateDeliverer = await _delivererService.GetById(1);
        Assert.That(deliverer!.User!.FirstName, Is.EqualTo(updateDeliverer!.User!.FirstName));
    }
}
