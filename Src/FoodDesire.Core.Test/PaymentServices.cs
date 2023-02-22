namespace FoodDesire.Core.Test;
[TestFixture]
public class PaymentServices : Services {
    public PaymentServices() : base("PaymentServices") { }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _adminService.CreateAccount(UserDataHelper.GetAdminPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _supplierService.CreateAccount(UserDataHelper.GetSupplierPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        await _delivererService.CreateAccount(UserDataHelper.GetDelivererPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 1,
            Order = new Order() {
                CustomerId = 1
            }
        });
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 2,
            Order = new Order() {
                CustomerId = 1
            }
        });
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task MakePaymentForOrder() {
        Payment payment = await _paymentService.PaymentForOrder(1);
        Order order = await _orderService.GetOrderById(1);

        Assert.That(order.Price, Is.EqualTo(payment.Value));
    }

    [Test, Order(2)]
    public async Task MakePaymentForOrderWithDelivery() {
        Delivery delivery = await _orderDeliveryServices.NewDeliveryForOrder(new Delivery() {
            OrderId = 2,
            DelivererId = 1,
            Fee = 210
        });

        Payment payment = await _paymentService.PaymentForOrder(2);
        Order order = await _orderService.GetOrderById(2);

        Assert.That(order.Price + order.Delivery!.Fee, Is.EqualTo(payment.Value));
    }

    //Payment for supply is tested in IngredientServices.cs

    [Test, Order(3)]
    public async Task PayChef() {
        Chef chef = await _chefService.GetById(1);
        Payment payment = new Payment() {
            Value = 1000,
            EmployeeId = chef.EmployeeId
        };
        Payment paid = await _paymentService.SalaryForEmployee(payment);
        chef = await _chefService.GetById(1);

        Assert.That(chef.Employee!.Salaries!.Sum(e => e.Value), Is.EqualTo(1000));
    }

    [Test, Order(4)]
    public async Task PaySupplier() {
        Supplier supplier = await _supplierService.GetById(1);
        Payment payment = new Payment() {
            Value = 1000,
            EmployeeId = supplier.EmployeeId
        };
        Payment paid = await _paymentService.SalaryForEmployee(payment);
        supplier = await _supplierService.GetById(1);

        Assert.That(supplier.Employee!.Salaries!.Sum(e => e.Value), Is.EqualTo(1000));
    }

    [Test, Order(5)]
    public async Task PayDeliverer() {
        Deliverer deliverer = await _delivererService.GetById(1);
        Payment payment = new Payment() {
            Value = 1000,
            EmployeeId = deliverer.EmployeeId
        };
        Payment paid = await _paymentService.SalaryForEmployee(payment);
        deliverer = await _delivererService.GetById(1);

        Assert.That(deliverer.Employee!.Salaries!.Sum(e => e.Value), Is.EqualTo(1000));
    }

    [Test, Order(6)]
    public async Task PayForMaintenance() {
        Payment payment = await _paymentService.PaymentForMaintenance(1200, "Monthly Electricity bill");
        payment = await _paymentService.PaymentForMaintenance(1200, "Monthly Water bill");

        List<Payment> payments = await _paymentService.GetExpenses();

        Assert.That(payments.Sum(p => p.Value), Is.EqualTo(5400)); // 4400 is the sum of the all payments excluding the orders
    }

    [Test, Order(7)]
    public async Task GetIncome() {
        List<Payment> payments = await _paymentService.GetIncome();

        Assert.That(payments.Sum(p => p.Value), expression: Is.AtLeast(1000));
    }
}
