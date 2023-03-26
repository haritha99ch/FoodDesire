using AutoMapper;

namespace FoodDesire.IMS.ViewModels;
public class EditIngredientViewModel : IngredientForm, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly int _ingredientId;
    private Ingredient _ingredient = new() { Name = "", Description = "" };

    public EditIngredientViewModel(int ingredientId) {
        _ingredientsPageService = App.GetService<IIngredientsPageService>();
        _ingredientId = ingredientId;
        _ = OnInit();
    }

    public async Task OnInit() {
        _ingredient = await _ingredientsPageService.GetIngredientById(_ingredientId);
        List<IngredientCategory> ingredientCategories = await _ingredientsPageService.GetAllIngredientCategory();
        ingredientCategories.ForEach(IngredientCategories.Add);
        IngredientName = _ingredient.Name;
        IngredientDescription = _ingredient.Description;
        IngredientMaximumQuantity = _ingredient.MaximumQuantity;
        Measurement = _ingredient.Measurement;
        Category = IngredientCategories.SingleOrDefault(e => e.Id == _ingredient.IngredientCategoryId)!.Name;
        IsLoading = false;
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

    public override async void AddNewIngredientCategory() {
        IsCategoriesLoaded = false;
        IngredientCategory category = new() {
            Name = NewIngredientCategoryName!,
            Description = NewIngredientCategoryDescription!
        };
        category = await _ingredientsPageService.AddIngredientCategory(category);
        IngredientCategories.Add(category);
        NewIngredientCategory = category;
        NewIngredientCategoryName = "";
        NewIngredientCategoryDescription = "";
        IsCategoriesLoaded = true;
    }

    public override async void EditIngredientCategory() {
        IsCategoriesLoaded = false;
        IngredientCategory category = IngredientCategories.SingleOrDefault(e => e.Name.Equals(Category))!;
        IngredientCategories.Remove(category);
        category.Name = NewIngredientCategoryName!;
        category.Description = NewIngredientCategoryDescription!;
        category = await _ingredientsPageService.EditIngredientCategory(category);
        IngredientCategories.Add(category);
        NewIngredientCategory = category;
        NewIngredientCategoryName = "";
        NewIngredientCategoryDescription = "";
        IsCategoriesLoaded = true;
    }

    public override async void DeleteIngredientCategory() {
        IsCategoriesLoaded = false;
        bool deleted = await _ingredientsPageService.DeleteIngredientCategory((int)_ingredientCategoryId!);
        NewIngredientCategory = IngredientCategories.SingleOrDefault(e => e.Name.Equals(Category))!;
        IngredientCategories.Remove(NewIngredientCategory);
        Categories.Remove(Category!);
        IsCategoriesLoaded = true;
    }
}
