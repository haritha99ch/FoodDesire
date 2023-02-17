namespace FoodDesire.Core.Test;
[TestFixture]
public class FoodOrderServices {
    private readonly IFoodItemService _foodItemService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeService _recipeService;
    private readonly IChefService _chefService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly FoodDesireContext _context;
    public FoodOrderServices() {
        ApplicationHostHelper.ConfigureHost("FoodOrderServices");

        _foodItemService = ApplicationHostHelper.GetService<IFoodItemService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _customerService = ApplicationHostHelper.GetService<ICustomerService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        foreach(var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach(var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewOrder() {
        Order order = new Order {
            CustomerId = 1,
        };
        order = await _orderService.NewOrder(order);
        Assert.That(order, Is.Not.Null);
    }

    [Test, Order(2)]
    public async Task NewFoodItem() {
        FoodItem foodItem = new FoodItem {
            RecipeId = 2,
            OrderId = 1,
        };
        foodItem = await _foodItemService.NewFoodItem(foodItem);
        Assert.That(foodItem.Price, Is.EqualTo(foodItem.Recipe!.FixedPrice));
    }

    [Test, Order(3)]
    public async Task AddNewFoodItemToOrder() {
        FoodItem foodItem = new FoodItem {
            RecipeId = 1,
            OrderId = 1,
        };
        await _foodItemService.NewFoodItem(foodItem);
        Order order = await _orderService.GetOrderById(1);
        Assert.That(order.FoodItems!, Has.Count.EqualTo(2));
    }

    [Test, Order(4)]
    public async Task ModifyFoodItem() {
        Order order = await _orderService.GetOrderById(1);
        /*
        [{"Recipe_Id":1,"Ingredient_Id":null,"Amount":8.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":true,"PricePerMultiplier":325.5,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":2,"Amount":200.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":true,"PricePerMultiplier":150.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":3,"Amount":4.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":1.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":4,"Amount":300.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":300.0,"Multiplier":1.0}]
         */
        FoodItem? selectedFoodItem = order.FoodItems![0];
        selectedFoodItem.FoodItemIngredients[0].Multiplier = 2;     //Add 325.5 (recipe as an ingredient recipeId = 1)
        selectedFoodItem.FoodItemIngredients[1].Multiplier = 0.5;   //Sub 150/2 (ingredientId = 2)
        selectedFoodItem.FoodItemIngredients[2].Multiplier = 2;     //Nothing should be changed (ingredientId = 3) "CanModify":false

        FoodItem updatedFoodItem = await _foodItemService.UpdateFoodItem(selectedFoodItem);
        Assert.That(updatedFoodItem.Price, Is.EqualTo(3055m + 325.5m - (150.0m / 2)));

        /*  After Update
        [{"Recipe_Id":1,"Ingredient_Id":null,"Amount":8.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":true,"PricePerMultiplier":325.5,"Multiplier":2.0},
        {"Recipe_Id":null,"Ingredient_Id":2,"Amount":200.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":true,"PricePerMultiplier":150.0,"Multiplier":0.5},
        {"Recipe_Id":null,"Ingredient_Id":3,"Amount":4.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":1.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":4,"Amount":300.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":300.0,"Multiplier":1.0}]
         */
    }

    [Test, Order(5)]
    public async Task AddOptionalIngredientToFoodItem() {
        Order order = await _orderService.GetOrderById(1);
        /*
        [{"Recipe_Id":null,"Ingredient_Id":1,"Amount":200.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":100.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":2,"Amount":100.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":75.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":3,"Amount":2.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":0.5,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":4,"Amount":150.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":150.0,"Multiplier":1.0},    
        {"Recipe_Id":null,"Ingredient_Id":5,"Amount":150.0,"RecommendedMultiplier":1.33,"IsRequired":false,"CanModify":true,"PricePerMultiplier":187.5,"Multiplier":0.0}]
         */
        FoodItem selectedFoodItem = order.FoodItems![1];
        selectedFoodItem.FoodItemIngredients[4].Multiplier = 2; //Add 187.5 x 2 (IngredientId = 5)

        FoodItem updatedFoodItem = await _foodItemService.UpdateFoodItem(selectedFoodItem);
        Assert.That(updatedFoodItem.Price, Is.EqualTo(325.50m + (187.5m * 2)));
        /*  After Update
        [{"Recipe_Id":null,"Ingredient_Id":1,"Amount":200.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":100.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":2,"Amount":100.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":75.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":3,"Amount":2.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":0.5,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":4,"Amount":150.0,"RecommendedMultiplier":1.0,"IsRequired":true,"CanModify":false,"PricePerMultiplier":150.0,"Multiplier":1.0},
        {"Recipe_Id":null,"Ingredient_Id":5,"Amount":150.0,"RecommendedMultiplier":1.33,"IsRequired":false,"CanModify":true,"PricePerMultiplier":187.5,"Multiplier":2.0}]
         */
    }
}
