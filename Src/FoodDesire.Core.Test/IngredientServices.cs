namespace FoodDesire.Core.Test;
[TestFixture]
public class IngredientServices {
    private ISupplierService _supplierService;
    private IIngredientService _ingredientService;

    public IngredientServices(ISupplierService supplierService, IIngredientService ingredientService) {
        _supplierService = supplierService;
        _ingredientService = ingredientService;
    }

    public async Task NewIngredient() {
        IngredientCategory? ingredientCategory = await _ingredientService.NewIngredientCategory(new IngredientCategory() {
            Name = "Key Ingredients",
            Description = "Key Ingredients",
        });
        Assert.That(ingredientCategory.Name, Is.EqualTo("Key Ingredients"));
    }
}
