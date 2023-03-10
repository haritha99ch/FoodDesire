namespace FoodDesire.Core.Test;
[TestFixture]
public class OrderDeliveryServices : Services {
    public OrderDeliveryServices() : base("OrderDeliveryServices") { }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(
            IngredientDataHelper.GetIngredientCategoryPayload()
        );
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        await _delivererService.CreateAccount(UserDataHelper.GetDelivererPayload());
        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
        // Create ann order and add a foodItem

        FoodItem foodItem = new() {
            RecipeId = 1,
            Order = new() {
                CustomerId = 1
            }
        };
        await _foodItemService.NewFoodItem(foodItem);
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 2,
            OrderId = 1
        });
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewDelivery() {
        Delivery delivery = new Delivery {
            OrderId = 1,
            Address = new Address {
                City = "Lagos",
                Street1 = "Ogunlana Drive",
                Street2 = "Surulere",
                No = "No 1",
            },
            DelivererId = 1,
        };
        Delivery newDelivery = await _orderDeliveryServices.NewDeliveryForOrder(delivery);
        newDelivery = await _orderDeliveryServices.GetDeliveryById(newDelivery.Id);

        Assert.That(newDelivery.IsDelivered, Is.False);
        Assert.That(newDelivery.Order!.FoodItems, Has.Count.EqualTo(2));
        Assert.That(newDelivery, Is.Not.Null);
    }

    [Test, Order(2)]
    public async Task GetOrderToDeliver() {
        FoodItem foodItem = new() {
            RecipeId = 1,
            Order = new() {
                CustomerId = 1
            }
        };
        await _foodItemService.NewFoodItem(foodItem);
        List<Order> orders = await _orderDeliveryServices.GetAllOrdersToDeliver();
        Assert.That(orders, Has.Count.EqualTo(2));
    }

    [Test, Order(3)]
    public async Task SetAsDelivered() {
        Delivery? delivered = await _orderDeliveryServices.OrderIsDelivered(1);
        Assert.That(delivered!.IsDelivered, Is.True);

        List<Order> orders = await _orderDeliveryServices.GetAllOrdersToDeliver();
        Assert.That(orders, Has.Count.EqualTo(1));
    }

    [Test, Order(4)]
    public async Task GetAllDeliveredOrders() {
        List<Order> orders = await _orderDeliveryServices.GetAllDeliveredOrders();
        Assert.That(orders, Has.Count.EqualTo(1));
    }
}
