namespace FoodDesire.Core.Test.Tests;
[TestFixture]
public class UserServices {
    private readonly IAdminService _adminService;
    private readonly ISupplierService _supplierService;
    private readonly IChefService _chefService;
    private readonly IDelivererService _delivererService;

    public UserServices(
        IAdminService adminService,
        ISupplierService supplierService,
        IChefService chefService,
        IDelivererService delivererService
        ) {
        _adminService = adminService;
        _supplierService = supplierService;
        _chefService = chefService;
        _delivererService = delivererService;
    }


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
        Admin saveAdmin = await _adminService.CreateAccount(admin);
        Assert.That(admin.User.FirstName, Is.EqualTo(saveAdmin.User!.FirstName));
    }

    public async Task LoginAsAdmin() {
        Admin admin = await _adminService.GetByEmailAndPassword("admin@fooddesire.com", "1234");
        Assert.IsNotNull(admin);
    }

    public async Task UpdateAdmin() {
        Admin admin = await _adminService.GetByIdPopulated(1);
        admin.User!.FirstName = "AdminUpdated";
        await _adminService.UpdateAccount(admin);
        Admin updateAdmin = await _adminService.GetByIdPopulated(1);
        Assert.That(admin.User.FirstName, Is.EqualTo(updateAdmin.User!.FirstName));
    }


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
        Supplier saveSupplier = await _supplierService.CreateAccount(supplier);
        Assert.That(supplier.Employee.User.FirstName, Is.EqualTo(saveSupplier.Employee!.User!.FirstName));
    }

    public async Task LoginAsSupplier() {
        Supplier supplier = await _supplierService.GetByEmailAndPassword("supplier@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }

    public async Task UpdateSupplier() {
        Supplier supplier = await _supplierService.GetByIdPopulated(1);
        supplier.Employee!.User!.FirstName = "SupplierUpdated";
        await _supplierService.UpdateAccount(supplier);
        Supplier updateAdmin = await _supplierService.GetByIdPopulated(1);
        Assert.That(supplier.Employee!.User!.FirstName, Is.EqualTo(updateAdmin.Employee!.User!.FirstName));
    }


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
        Chef saveChef = await _chefService.CreateAccount(chef);
        Assert.That(chef.Employee.User.FirstName, Is.EqualTo(saveChef.Employee!.User!.FirstName));
    }

    public async Task LoginAsChef() {
        Chef supplier = await _chefService.GetByEmailAndPassword("chef@fooddesire.com", "1234");
        Assert.IsNotNull(supplier);
    }

    public async Task UpdateChef() {
        Chef chef = await _chefService.GetByIdPopulated(1);
        chef.Employee!.User!.FirstName = "ChefUpdated";
        await _chefService.UpdateAccount(chef);
        Chef updateSupplier = await _chefService.GetByIdPopulated(1);
        Assert.That(chef.Employee!.User!.FirstName, Is.EqualTo(updateSupplier.Employee!.User!.FirstName));
    }

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
        Deliverer saveDeliverer = await _delivererService.CreateAccount(deliverer);
        Assert.That(deliverer.Employee.User.FirstName, Is.EqualTo(saveDeliverer.Employee!.User!.FirstName));
    }

    public async Task LoginAsDeliverer() {
        Deliverer deliverer = await _delivererService.GetByEmailAndPassword("deliverer@fooddesire.com", "1234");
        Assert.IsNotNull(deliverer);
    }

    public async Task UpdateDeliverer() {
        Deliverer deliverer = await _delivererService.GetByIdPopulated(1);
        deliverer.Employee!.User!.FirstName = "DelivererUpdated";
        await _delivererService.UpdateAccount(deliverer);
        Deliverer updateDeliverer = await _delivererService.GetByIdPopulated(1);
        Assert.That(deliverer.Employee!.User!.FirstName, Is.EqualTo(updateDeliverer.Employee!.User!.FirstName));
    }
}