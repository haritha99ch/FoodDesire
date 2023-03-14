using AutoMapper;

namespace FoodDesire.IMS.ViewModels;
public class EditIngredientViewModel : IngredientForm, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly IMapper _mapper;
    private Ingredient _ingredient;

    public EditIngredientViewModel(Ingredient ingredient) {
        _ingredientsPageService = App.GetService<IIngredientsPageService>();
        _mapper = App.GetService<IMapper>();
        _ingredient = ingredient;
        _ = OnInit();
    }

    public async Task OnInit() {
        List<IngredientCategory> ingredientCategories = await _ingredientsPageService.GetAllIngredientCategory();
        ingredientCategories.ForEach(IngredientCategories.Add);
        IngredientName = _ingredient.Name;
        IngredientDescription = _ingredient.Description;
        IngredientMaximumQuantity = _ingredient.MaximumQuantity;
        Measurement = _ingredient.Measurement;
        Category = IngredientCategories.SingleOrDefault(e => e.Id == _ingredient.Id)!.Name;

    }

    public async Task<Ingredient> EditIngredient() {
        _ingredient.Name = IngredientName!;
        _ingredient.Description = IngredientDescription!;
        _ingredient.IngredientCategoryId = (int)_ingredientCategoryId!;
        _ingredient.Measurement = Measurement;
        _ingredient.MaximumQuantity = (double)IngredientMaximumQuantity!;

        _ingredient = await _ingredientsPageService.EditIngredient(_ingredient);
        return _ingredient;
    }
}
