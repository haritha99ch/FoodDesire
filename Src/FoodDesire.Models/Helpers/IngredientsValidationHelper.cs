using FoodDesire.Models;
public class EitherIdExists: ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        var recipeIngredient = (RecipeIngredient)validationContext.ObjectInstance;
        if(recipeIngredient.RecipeId == null && recipeIngredient.IngredientId == null) {
            return new ValidationResult("Either RecipeId or IngredientId must be specified.");
        }
        if(recipeIngredient.RecipeId != null && recipeIngredient.IngredientId != null) {
            return new ValidationResult("Both RecipeId and IngredientId cannot be specified.");
        }
        return ValidationResult.Success!;
    }
}