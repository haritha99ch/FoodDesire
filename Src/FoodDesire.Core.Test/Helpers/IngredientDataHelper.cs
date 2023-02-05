namespace FoodDesire.Core.Test.Helpers;
public static class IngredientDataHelper {
    public static Ingredient GetIngredientPayLoad(int ingredientCategoryId) {
        return new Ingredient() {
            Name = "Salt",
            Description = "Salt",
            IngredientCategoryId = ingredientCategoryId,
            CurrentPricePerUnit = 0,
            CurrentQuantity = 0,
            Image = new Image() { Data = "Image data" },
            MaximumQuantity = 1000,
            Measurement = Measurement.Mass
        };
    }
}