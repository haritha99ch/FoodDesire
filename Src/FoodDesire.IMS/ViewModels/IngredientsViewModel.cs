using FoodDesire.Models;
using System.Diagnostics;

namespace FoodDesire.IMS.ViewModels;
public class IngredientsViewModel : ObservableRecipient {
    private readonly IIngredientsPageService _ingredientsPageService;
    private List<Ingredient> _ingredients = new List<Ingredient>();

    public List<Ingredient> Ingredients {
        get => _ingredients;
        set => SetProperty(ref _ingredients, value);
    }

    public IngredientsViewModel(IIngredientsPageService ingredientsPageService) {
        _ingredientsPageService = ingredientsPageService;
        _ = OnInit();
    }

    public async Task OnInit() {
        Ingredients = await _ingredientsPageService.GetAllIngredients();
        Debug.WriteLine("Hello");
        await _ingredientsPageService.AddIngredientCategory(new() {
            Name = "Main Meal",
            Description = "Main meals for breakfast, Lunch, Dinner",
        });
        await _ingredientsPageService.AddIngredient(new() {
            Name = "Flour",
            Description = "All-purpose flour",
            Measurement = Measurement.Grams,
            CurrentPricePerUnit = 0.5,
            MaximumQuantity = 1000,
            CurrentQuantity = 500,
            IngredientCategoryId = 1,
        });
    }
}
