namespace FoodDesire.Core.Test.Helpers;
public static class UserDataHelper {
    public static Supplier GetSupplierPayload() {
        return new() {
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
            City = "Diyatalawa"
        };
    }
    public static Admin GetAdminPayload() {
        return new Admin() {
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
    }

    public static Chef GetChefPayload() {
        return new() {
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
        };
    }

    public static Customer GetCustomerPayload() {
        return new() {
            User = new User() {
                FirstName = "Customer",
                LastName = "Remotsuc",
                DateOfBirth = new DateTime(1999, 5, 2),
                Account = new Account() {
                    Email = "customer@gmail.com",
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
    }

    public static Deliverer GetDelivererPayload() {
        return new Deliverer() {
            User = new User() {
                FirstName = "Deliverer",
                LastName = "Rerevilved",
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
            VehicleType = VehicleType.Bike,
            LicenseNo = "ASD 2314"
        };
    }
}
