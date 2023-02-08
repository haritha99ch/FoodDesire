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
            RecipeCategoryId = 1,
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
            IngredientId = ingredientId,
            Amount = 50,
            PricePerMultiplier = 1,
            RecommendedAmount = 50,
            IsRequired = true,
        };
    }

    internal static List<Ingredient> GetIngredients() {
        return new List<Ingredient>{
            new Ingredient {
                Name = "Flour",
                Description = "All-purpose flour",
                Measurement = Measurement.Grams,
                CurrentPricePerUnit = 0.5,
                MaximumQuantity = 1000,
                CurrentQuantity = 500,
                IngredientCategoryId = 1,
            },
            new Ingredient {
                Name = "Sugar",
                Description = "Granulated white sugar",
                Measurement = Measurement.Grams,
                CurrentPricePerUnit = 0.75,
                MaximumQuantity = 500,
                CurrentQuantity = 200,
                IngredientCategoryId = 1,
            },
            new Ingredient {
                Name = "Eggs",
                Description = "Grade A large eggs",
                Measurement = Measurement.Each,
                CurrentPricePerUnit = 0.25,
                MaximumQuantity = 200,
                CurrentQuantity = 150,
                IngredientCategoryId = 1,
            },
            new Ingredient {
                Name = "Butter",
                Description = "Unsalted sweet cream butter",
                Measurement = Measurement.Grams,
                CurrentPricePerUnit = 1.0,
                MaximumQuantity = 400,
                CurrentQuantity = 300,
                IngredientCategoryId = 1,
            },
        };
    }

    internal static Recipe GetRecipe() {
        return new Recipe {
            ChefId = 1,
            Name = "Chocolate Chip Cookies",
            Description = "Soft and chewy chocolate chip cookies",
            MinimumPrice = 3.0m,
            FixedPrice = 3.5m,
            RecipeCategoryId = 1,
            Tags = "dessert, sweet, snack",
            RecipeIngredients = new List<RecipeIngredient>{
                new RecipeIngredient {
                    RecipeId = 1,
                    IngredientId = 1,
                    Amount = 200,
                    RecommendedAmount = 200,
                    IsRequired = true,
                    PricePerMultiplier = 0.5m
                },
                new RecipeIngredient {
                    RecipeId = 1,
                    IngredientId = 2,
                    Amount = 100,
                    RecommendedAmount = 100,
                    IsRequired = true,
                    PricePerMultiplier = 0.75m
                },
                new RecipeIngredient {
                    RecipeId = 1,
                    IngredientId = 3,
                    Amount = 2,
                    RecommendedAmount = 2,
                    IsRequired = true,
                    PricePerMultiplier = 0.25m
                },
                new RecipeIngredient {
                    RecipeId = 1,
                    IngredientId = 4,
                    Amount = 150,
                    RecommendedAmount = 150,
                    IsRequired = true,
                    PricePerMultiplier = 1.0m
                }
            }
        };
    }
}
