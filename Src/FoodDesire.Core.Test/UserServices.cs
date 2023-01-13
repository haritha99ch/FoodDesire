namespace FoodDesire.Core.Test;
[TestFixture]
public class UserServices {
    private IAdminService adminService;
    private ISupplierService supplierService;
    private IChefService chefService;
    private IRepository<Deliverer> _delivererRepository;
    private IDelivererService delivererService;
    private IRepository<Admin> _adminRepository;
    private IRepository<Supplier> _supplierRepository;
    private IRepository<Chef> _chefRepository;
    private ITrackingRepository<User> _userTRepository;
    private protected FoodDesireContext _context;

    [OneTimeSetUp]
    public void Setup() {
        _context = DbContextHelper.GetContext("UserServices");
        if(!_context.Database.EnsureCreated()) return;
        _userTRepository = new TrackingRepository<User>(_context);

        _adminRepository = new Repository<Admin>(_context);
        adminService = new AdminService(_adminRepository, _userTRepository);

        _supplierRepository = new Repository<Supplier>(_context);
        supplierService = new SupplierService(_supplierRepository, _userTRepository);

        _chefRepository = new Repository<Chef>(_context);
        chefService = new ChefService(_chefRepository, _userTRepository);

        _delivererRepository = new Repository<Deliverer>(_context);
        delivererService = new DelivererService(_delivererRepository, _userTRepository);
    }

    [OneTimeTearDown]
    public void TearDown() {
        _context.Database.EnsureDeleted();
    }
    [Test, Order(1)]
    public async Task CreateAdmin() {
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

    [Test, Order(4)]
    public async Task CreateSupplier() {
        Supplier supplier = new() {
            Employee = new Employee {
                User = new User() {
                    FirstName = "Supplier",
                    LastName = "Reilppus",
                    DateOfBirth = new DateTime(1999, 5, 2),
                    Account = new Account() {
                        Email = "supplier@fooddesire.com",
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
            },
            City = "Diyatalawa"
        };
        Supplier saveSupplier = await supplierService.CreateAccount(supplier);
        Assert.That(supplier.Employee.User.FirstName, Is.EqualTo(saveSupplier.Employee!.User!.FirstName));
    }
    [Test, Order(5)]
    public async Task LoginAsSupplier() {
        Supplier supplier = await supplierService.GetByEmailAndPassword("supplier@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }
    [Test, Order(6)]
    public async Task UpdateSupplier() {
        Supplier supplier = await supplierService.GetByIdPopulated(1);
        supplier.Employee!.User!.FirstName = "SupplierUpdated";
        await supplierService.UpdateAccount(supplier);
        Supplier updateAdmin = await supplierService.GetByIdPopulated(1);
        Assert.That(supplier.Employee!.User!.FirstName, Is.EqualTo(updateAdmin.Employee!.User!.FirstName));
    }

    [Test, Order(7)]
    public async Task CreateChef() {
        Chef chef = new() {
            Employee = new Employee {
                User = new User() {
                    FirstName = "Chef",
                    LastName = "Fehc",
                    DateOfBirth = new DateTime(1999, 5, 2),
                    Account = new Account() {
                        Email = "chef@fooddesire.com",
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
            },
        };
        Chef saveChef = await chefService.CreateAccount(chef);
        Assert.That(chef.Employee.User.FirstName, Is.EqualTo(saveChef.Employee!.User!.FirstName));
    }
    [Test, Order(8)]
    public async Task LoginAsChef() {
        Chef supplier = await chefService.GetByEmailAndPassword("chef@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }
    [Test, Order(9)]
    public async Task UpdateChef() {
        Chef chef = await chefService.GetByIdPopulated(1);
        chef.Employee!.User!.FirstName = "ChefUpdated";
        await chefService.UpdateAccount(chef);
        Chef updateSupplier = await chefService.GetByIdPopulated(1);
        Assert.That(chef.Employee!.User!.FirstName, Is.EqualTo(updateSupplier.Employee!.User!.FirstName));
    }
    [Test, Order(10)]
    public async Task CreateDeliverer() {
        Deliverer deliverer = new() {
            Employee = new Employee {
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
            },
            LicenseNo = "2",
        };
        Deliverer saveDeliverer = await delivererService.CreateAccount(deliverer);
        Assert.That(deliverer.Employee.User.FirstName, Is.EqualTo(saveDeliverer.Employee!.User!.FirstName));
    }
    [Test, Order(11)]
    public async Task LoginAsDeliverer() {
        Deliverer deliverer = await delivererService.GetByEmailAndPassword("deliverer@fooddesire.com", "1234");
        Assert.IsNotNull(deliverer);
    }
    [Test, Order(12)]
    public async Task UpdateDeliverer() {
        Deliverer deliverer = await delivererService.GetByIdPopulated(1);
        deliverer.Employee!.User!.FirstName = "DelivererUpdated";
        await delivererService.UpdateAccount(deliverer);
        Deliverer updateDeliverer = await delivererService.GetByIdPopulated(1);
        Assert.That(deliverer.Employee!.User!.FirstName, Is.EqualTo(updateDeliverer.Employee!.User!.FirstName));
    }
}