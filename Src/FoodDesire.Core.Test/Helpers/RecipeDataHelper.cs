namespace FoodDesire.Core.Test.Helpers;
internal static class RecipeDataHelper {
    internal static RecipeCategory GetRecipeCategoryPayload() {
        return new RecipeCategory() {
            Name = "Main Meal",
            Description = "Main meals for breakfast, Lunch, Dinner",
        };
    }
    internal static Recipe GetRecipePayload() {
        return new Recipe() {
            Name = "Fried Rice",
            Description = "A rice fried in oil",
            ChefId = 1,
            FoodCategoryId = 1,
            RecipeIngredients = {
                GetRecipeIngredientPayload(1),
                GetRecipeIngredientPayload(2)
            },
            Image = new Image() {
                Data = "Image data"
            },
        };
    }
    internal static RecipeIngredient GetRecipeIngredientPayload(int ingredientId) {
        return new RecipeIngredient() {
            IngredeintId = ingredientId,
            Amount = 50,
            PricePerMultiplier = 10,
            RecommendedAmount = 50,
            IsRequired = true,
        };
    }
}
