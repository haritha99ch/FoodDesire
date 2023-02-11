namespace FoodDesire.Core.Test;
[TestFixture]
public class IngredientServices {
    private readonly ISupplierService _supplierService;
    private readonly IIngredientService _ingredientService;
    private readonly IAdminService _adminService;
    private readonly FoodDesireContext _context;

    public IngredientServices() {
        ApplicationHostHelper.ConfigureHost("IngredientServices");

        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _supplierService = ApplicationHostHelper.GetService<ISupplierService>();
        _adminService = ApplicationHostHelper.GetService<IAdminService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task SetUp() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _supplierService.CreateAccount(UserDataHelper.GetSupplierPayload());
        //To make NewSupply service work an admin should be registered in the system.
        await _adminService.CreateAccount(UserDataHelper.GetAdminPayload());
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1), Description("Should create a new Ingredient category")]
    public async Task NewIngredientCategory() {
        IngredientCategory? ingredientCategory = await _ingredientService
            .NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        Assert.That(ingredientCategory.Name, Is.EqualTo("Key"));

    }



    [Test, Order(2)]
    public async Task NewIngredient() {
        Ingredient newIngredient = IngredientDataHelper.GetIngredientPayLoad(1);
        Ingredient savedIngredient = await _ingredientService.NewIngredient(newIngredient);
        Assert.That(savedIngredient.Name, Is.EqualTo(newIngredient.Name));

        newIngredient = await _ingredientService.GetIngredientByName(IngredientDataHelper.GetIngredientPayLoad(1).Name);
        Assert.That(newIngredient.Name, Is.EqualTo(IngredientDataHelper.GetIngredientPayLoad(1).Name));

        IngredientCategory? ingredientCategory = await _ingredientService.GetIngredientCategoryByName("Key");
        Assert.That(ingredientCategory.Name, Is.EqualTo("Key"));
    }

    [Test, Order(3)]
    public async Task GetAllIngredientsAndCategories() {
        Ingredient newIngredient = IngredientDataHelper.GetIngredientPayLoad(1);
        newIngredient.Measurement = Measurement.Liquid;
        await _ingredientService.NewIngredient(newIngredient);

        List<Ingredient> ingredients = await _ingredientService.GetAllIngredients();
        Assert.That(ingredients.Count, Is.EqualTo(2));

        List<IngredientCategory> ingredientCategories = await _ingredientService.GetAllIngredientCategories();
        Assert.That(ingredientCategories.Count, Is.EqualTo(1));
    }

    [Test, Order(4)]
    public async Task GetIngredientAndCategoryById() {
        Ingredient ingredient = await _ingredientService.GetIngredientById(1);
        Assert.That(ingredient.Name, Is.EqualTo(IngredientDataHelper.GetIngredientPayLoad(1).Name));

        IngredientCategory ingredientCategory = await _ingredientService.GetIngredientCategoryById(1);
        Assert.That(ingredientCategory.Name, Is.EqualTo("Key"));
    }

    [Test, Order(5)]
    public async Task GetAllIngredientsByCategory() {
        List<Ingredient>? ingredients = await _ingredientService.GetAllIngredientsByCategory("Key");

        Assert.That(ingredients[1].IngredientCategory!.Name, Is.EqualTo("Key"));
    }

    [Test, Order(6)]
    public async Task NewSupply() {
        Supply supply = new Supply() {
            IngredientId = 1,
            Amount = 1000,
            SuppliedDate = new DateTime(),
            SupplierId = 1,
        };
        Supply newSupply = await _ingredientService.NewSupply(supply, 3000);

        Ingredient ingredient = await _ingredientService.GetIngredientById(1);
        Assert.That(ingredient.CurrentQuantity, Is.EqualTo(1000));
    }
    [Test, Order(7)]
    public async Task DeleteIngredientCategory() {
        bool categoryDeleted = await _ingredientService.DeleteIngredientCategoryById(1);

        Assert.That(categoryDeleted, Is.True);
    }
}
