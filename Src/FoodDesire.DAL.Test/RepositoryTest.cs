namespace FoodDesire.DAL.Test;
[TestFixture]
public class RepositoryTest {
    private IRepository<Customer> customerRepository;
    private IRepository<Admin> adminRepository;
    private IRepository<Chef> chefRepository;
    private IRepository<Supplier> supplierRepository;
    private IRepository<Deliverer> delivererRepository;
    private ITrackingRepository<User> userTrackingRepository;
    private IRepository<User> userRepository;
    private IRepository<Account> accountRepository;
    private protected FoodDesireContext _context;

    [OneTimeSetUp]
    public void Setup() {
        _context = DbContextHelper.GetContext("Repository");
        if(!_context.Database.EnsureCreated()) return;
        customerRepository = new Repository<Customer>(_context);
        adminRepository = new Repository<Admin>(_context);
        chefRepository = new Repository<Chef>(_context);
        supplierRepository = new Repository<Supplier>(_context);
        delivererRepository = new Repository<Deliverer>(_context);
        accountRepository = new Repository<Account>(_context);
        userTrackingRepository = new TrackingRepository<User>(_context);
        userRepository = new Repository<User>(_context);
    }

    [OneTimeTearDown]
    public void TearDown() {
        _context.Database.EnsureDeleted();
    }

    [Test, Order(1)]
    public async Task AddAllCustomer() {
        List<Customer> customers = new List<Customer>() {
            new Customer() {
            User = new User() {
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = new DateTime(1999, 6, 18),
                Gender = Gender.Male,
                Address = new Address() {
                    No = "1/156",
                    Street1 = "Nilmini",
                    Street2 = "Kahagolla",
                    City = "Diyatalawa",
                    PostalCode = 19150
                },
                Account = new Account() {
                    Email = "customer@outlook.com",
                    Password = "asd123",
                },
            },
        },new Customer() {
            User = new User() {
                FirstName = "Test2",
                LastName = "Test2",
                DateOfBirth = new DateTime(1999, 6, 18),
                Gender = Gender.Female,
                Address = new Address() {
                    No = "1/156",
                    Street1 = "Nilmini",
                    Street2 = "Kahagolla",
                    City = "Diyatalawa",
                    PostalCode = 19150
                },
                Account = new Account() {
                    Email = "customer2@outlook.com",
                    Password = "asd123",
                },
            },
        }
        };
        List<Customer> newCustomers = await customerRepository.AddAll(customers);

        Assert.That(newCustomers.Count, Is.EqualTo(customers.Count));
    }

    [Test, Order(2)]
    public async Task CreateAdmin() {
        Admin admin = new Admin() {
            User = new User() {
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = new DateTime(1999, 6, 18),
                Gender = Gender.Male,
                Address = new Address() {
                    No = "1/156",
                    Street1 = "Nilmini",
                    Street2 = "Kahagolla",
                    City = "Diyatalawa",
                    PostalCode = 19150
                },
                Account = new Account() {
                    Email = "amin@outlook.com",
                    Password = "asd123",
                },
            },
        };
        Admin newAdmin = await adminRepository.Add(admin);

        Assert.That(newAdmin!, Is.EqualTo(admin!));
    }

    [Test, Order(3)]
    public async Task CreateChef() {
        Chef chef = new Chef() {
            Employee = new Employee() {
                User = new User() {
                    FirstName = "Test",
                    LastName = "Test",
                    DateOfBirth = new DateTime(1999, 6, 18),
                    Gender = Gender.Male,
                    Address = new Address() {
                        No = "1/156",
                        Street1 = "Nilmini",
                        Street2 = "Kahagolla",
                        City = "Diyatalawa",
                        PostalCode = 19150
                    },
                    Account = new Account() {
                        Email = "chef@outlook.com",
                        Password = "asd123",
                    },
                },
            },
        };
        Chef newChef = await chefRepository.Add(chef);

        Assert.That(newChef!, Is.EqualTo(chef!));
    }

    [Test, Order(4)]
    public async Task CreateSupplier() {
        Supplier supplier = new Supplier() {
            Employee = new Employee() {
                User = new User() {
                    FirstName = "Test",
                    LastName = "Test",
                    DateOfBirth = new DateTime(1999, 6, 18),
                    Gender = Gender.Male,
                    Address = new Address() {
                        No = "1/156",
                        Street1 = "Nilmini",
                        Street2 = "Kahagolla",
                        City = "Diyatalawa",
                        PostalCode = 19150
                    },
                    Account = new Account() {
                        Email = "supplier@outlook.com",
                        Password = "asd123",
                    },
                },
            },
            City = "Diyatalawa"
        };
        Supplier newSupplier = await supplierRepository.Add(supplier);

        Assert.That(newSupplier!, Is.EqualTo(supplier!));
    }

    [Test, Order(5)]
    public async Task CreateDeliverer() {
        Deliverer deliverer = new Deliverer() {
            Employee = new Employee() {
                User = new User() {
                    FirstName = "Test",
                    LastName = "Test",
                    DateOfBirth = new DateTime(1999, 6, 18),
                    Gender = Gender.Male,
                    Address = new Address() {
                        No = "1/156",
                        Street1 = "Nilmini",
                        Street2 = "Kahagolla",
                        City = "Diyatalawa",
                        PostalCode = 19150
                    },
                    Account = new Account() {
                        Email = "deliverer@outlook.com",
                        Password = "asd123",
                    },
                },
            },
            VehicleType = VehicleType.Bike,
            LicenseNo = "ASD 2314"
        };
        Deliverer newDeliverer = await delivererRepository.Add(deliverer);

        Assert.That(newDeliverer!, Is.EqualTo(deliverer!));
    }

    [Test, Order(6)]
    public async Task GetAllUsers() {
        List<User> users = await userTrackingRepository.GetAll();

        Assert.That(users.Count, Is.EqualTo(6));
    }

    [Test, Order(7)]
    public async Task GetAccountByEmailAndPassword() {
        Expression<Func<Account, bool>> filter =
            e => e.Email.Equals("deliverer@outlook.com") && e.Password.Equals("asd123");

        Account? account = await accountRepository.GetOne(filter);

        Assert.That(account.Email, Is.EqualTo("deliverer@outlook.com"));
    }

    [Test, Order(8)]
    public async Task GetAccountByInvalidEmailAndPassword() {
        Expression<Func<Account, bool>> filter =
            e => e.Email.Equals("deliverer@outlook.com") && e.Password.Equals("asdd123");

        Account? account = await accountRepository.GetOne(filter);

        Assert.IsNull(account);
    }

    [Test, Order(9)]
    public async Task GetUserById() {
        List<User>? users = await userTrackingRepository.GetAll();
        User user = users.FirstOrDefault()!;

        User getUser = await userTrackingRepository.GetByID(user!.Id);
        Assert.That(user.Id, Is.EqualTo(getUser.Id));
    }

    [Test, Order(10)]
    public async Task UpdateUser() {
        List<User> users = await userTrackingRepository.GetAll();
        User user = users.FirstOrDefault()!;

        User getUser = await userTrackingRepository.GetByID(user!.Id);
        getUser.FirstName = "Haritha";
        getUser.LastName = "Rathnayake";

        User updatedUser = await userTrackingRepository.Update(getUser);
        await userTrackingRepository.SaveChanges();
        getUser = await userTrackingRepository.GetByID(user!.Id);
        Assert.That(getUser.FirstName, Is.EqualTo(updatedUser.FirstName));
    }

    [Test, Order(11)]
    public async Task GetByFilteringAndOrdering() {
        Expression<Func<User, bool>> filter = e => !e.Deleted;
        Expression<Func<User, string>> order = e => e.FirstName;

        List<User> orderedUserList = await userTrackingRepository.Get(filter, order);

        Assert.That(orderedUserList.Count, Is.EqualTo(6));

    }

    [Test, Order(12)]
    public async Task DeleteById() {
        List<Customer> customers = await customerRepository.GetAll();

        bool entityDeleted = await customerRepository.Delete(customers[1].Id);

        Assert.That(entityDeleted, Is.EqualTo(true));

    }

    [Test, Order(13)]
    public async Task SotDelete() {
        List<User> users = await userTrackingRepository.GetAll();

        bool entityDeleted = await userTrackingRepository.SoftDelete(users[1].Id);
        await userTrackingRepository.SaveChanges();
        User user = await userRepository.GetByID(users[1].Id);
        Assert.IsTrue(user.Deleted);
    }

    [Test, Order(14)]
    public async Task GetAllTracking() {
        List<User> users = await userTrackingRepository.GetAll();

        Assert.That(users.Count, Is.EqualTo(5));
    }
}
