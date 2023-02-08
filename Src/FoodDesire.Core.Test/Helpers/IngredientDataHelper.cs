namespace FoodDesire.Core.Test.Helpers;
internal static class IngredientDataHelper {
    internal static Ingredient GetIngredientPayLoad(int ingredientCategoryId) {
        return new Ingredient() {
            Name = "Salt",
            Description = "Salt",
            IngredientCategoryId = ingredientCategoryId,
            CurrentPricePerUnit = 10,
            CurrentQuantity = 0,
            MaximumQuantity = 1000,
            Measurement = Measurement.Grams
        };
    }
    internal static IngredientCategory GetIngredientCategoryPayload() {
        return new IngredientCategory() {
            Name = "Key",
            Description = "Key Ingredients",
        };
    }
}