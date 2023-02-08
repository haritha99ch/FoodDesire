namespace FoodDesire.Core.Test.Helpers;
internal static class IngredientDataHelper {
    internal static Ingredient GetIngredientPayLoad(int ingredientCategoryId) {
        return new Ingredient() {
            Name = "Salt",
            Description = "Salt",
            IngredientCategoryId = ingredientCategoryId,
            CurrentPricePerUnit = 10,
            CurrentQuantity = 0,
            Image = new Image() { Data = "Image data" },
            MaximumQuantity = 1000,
            Measurement = Measurement.Mass
        };
    }
    internal static IngredientCategory GetIngredientCategoryPayload() {
        return new IngredientCategory() {
            Name = "Key",
            Description = "Key Ingredients",
        };
    }
}