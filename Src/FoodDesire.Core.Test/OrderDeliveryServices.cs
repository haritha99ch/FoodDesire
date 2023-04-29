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
                CustomerId = 1,
                Delivery = new Delivery {
                    Address = new Address {
                        City = "Lagos",
                        Street1 = "Ogunlana Drive",
                        Street2 = "Surulere",
                        No = "No 1",
                    },
                    DelivererId = 1,
                }
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

    [Test, Order(2)]
    public async Task GetOrderToDeliver() {
        await _foodItemService.FoodItemPrepared(1);
        await _foodItemService.FoodItemPrepared(2);
        List<Order> orders = await _orderDeliveryServices.GetAllOrdersToDeliver();
        Assert.That(orders, Has.Count.EqualTo(1));
    }

    [Test, Order(3)]
    public async Task SetAsDelivered() {
        Order? order = await _orderDeliveryServices.OrderIsDelivered(1);
        Assert.That(order.Delivery!.IsDelivered, Is.True);

        List<Order> orders = await _orderDeliveryServices.GetAllOrdersToDeliver();
        Assert.That(orders, Has.Count.EqualTo(0));
    }

    [Test, Order(4)]
    public async Task GetAllDeliveredOrders() {
        List<Order> orders = await _orderDeliveryServices.GetAllDeliveredOrders();
        Assert.That(orders, Has.Count.EqualTo(1));
    }
}
