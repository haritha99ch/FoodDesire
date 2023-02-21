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
    }
}
