using FoodDesire.Models;

namespace FoodDesire.IMS.ViewModels;
public class IngredientsViewModel : ObservableRecipient, IInitializable {
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
    }
}
