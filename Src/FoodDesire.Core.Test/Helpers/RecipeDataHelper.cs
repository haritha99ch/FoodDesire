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
            RecipeIngredients = { GetRecipeIngredientPayload(1), GetRecipeIngredientPayload(2) },
            Image = new Image() { Data = "Image data" },
        };
    }

    internal static RecipeIngredient GetRecipeIngredientPayload(int ingredientId) {
        return new RecipeIngredient() {
            Ingredient_Id = ingredientId,
            Amount = 50,
            PricePerMultiplier = 1,
            RecommendedAmount = 50,
            IsRequired = true,
        };
    }

    internal static List<Ingredient> GetIngredients() {
        return new List<Ingredient> {
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
            new Ingredient {
                Name = "Cocoa powder",
                Description = "Cocoa powder no sugar",
                Measurement = Measurement.Grams,
                CurrentPricePerUnit = 1.25,
                MaximumQuantity = 500,
                CurrentQuantity = 200,
                IngredientCategoryId = 1,
            }
        };
    }

    internal static List<Recipe> GetRecipes() {
        return new List<Recipe> {
            new Recipe {
                ChefId = 1,
                Name = "Double Chocolate Cookies",
                Description = "Rich and fudgy double chocolate cookies",
                MinimumPrice = 7.0m,
                FixedPrice = 7.5m,
                RecipeCategoryId = 1,
                Tags = "dessert, sweet, chocolate",
                RecipeIngredients = new List<RecipeIngredient> {
                    new RecipeIngredient {
                        Ingredient_Id = 1,
                        Amount = 200,
                        RecommendedAmount = 200,
                        IsRequired = true,
                        PricePerMultiplier = 0.5m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 2,
                        Amount = 100,
                        RecommendedAmount = 100,
                        IsRequired = true,
                        PricePerMultiplier = 0.75m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 3,
                        Amount = 2,
                        RecommendedAmount = 2,
                        IsRequired = true,
                        PricePerMultiplier = 0.25m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 4,
                        Amount = 150,
                        RecommendedAmount = 150,
                        IsRequired = true,
                        PricePerMultiplier = 1.0m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 5,
                        Amount = 150,
                        RecommendedAmount = 200,
                        IsRequired = false,
                        CanModify = true,
                        PricePerMultiplier = 0.5m
                    },
                }
            },
            new Recipe {
                ChefId = 1,
                Name = "Fruit Cake",
                Description = "A classic fruit cake with mixed dried fruit",
                MinimumPrice = 10.0m,
                FixedPrice = 12.0m,
                RecipeCategoryId = 1,
                Tags = "cake, fruit, holiday",
                RecipeIngredients = new List<RecipeIngredient> {
                    new RecipeIngredient {
                        Recipe_Id = 1,
                        Amount = 8,
                        RecommendedAmount = 8,
                        IsRequired = true,
                        CanModify = true,
                        PricePerMultiplier = 7.5m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 2,
                        Amount = 200,
                        RecommendedAmount = 200,
                        IsRequired = true,
                        CanModify = true,
                        PricePerMultiplier = 0.75m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 3,
                        Amount = 4,
                        RecommendedAmount = 4,
                        IsRequired = true,
                        PricePerMultiplier = 0.25m
                    },
                    new RecipeIngredient {
                        Ingredient_Id = 4,
                        Amount = 300,
                        RecommendedAmount = 300,
                        IsRequired = true,
                        PricePerMultiplier = 1.0m
                    }
                }
            }
        };
    }
}
