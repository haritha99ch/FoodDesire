﻿namespace FoodDesire.IMS.ViewModels;
public class NewIngredientFormViewModel : IngredientForm, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private Ingredient? _ingredient;

    public NewIngredientFormViewModel() {
        _ingredientsPageService = App.GetService<IIngredientsPageService>();
        OnInit();
    }

    public async void OnInit() {
        List<IngredientCategory> ingredientCategories = await _ingredientsPageService.GetAllIngredientCategory();
        ingredientCategories.ForEach(IngredientCategories.Add);
        Category = Categories.FirstOrDefault();
        IsLoading = false;
    }

    public async Task<Ingredient> CreateIngredient() {
        _ingredient = new() {
            Name = IngredientName!,
            Description = IngredientDescription!,
            IngredientCategoryId = (int)_ingredientCategoryId!,
            MaximumQuantity = (double)IngredientMaximumQuantity!
        };
        _ingredient = await _ingredientsPageService.AddIngredient(_ingredient);
        return _ingredient;
    }

    public override async void AddNewIngredientCategory(IIngredientsPageService ingredientsPageService) {
        IngredientCategory category = new() {
            Name = NewIngredientCategoryName!,
            Description = NewIngredientCategoryDescription!
        };
        category = await _ingredientsPageService.AddIngredientCategory(category);
        IngredientCategories.Add(category);
        NewIngredientCategory = category;
        NewIngredientCategoryName = "";
        NewIngredientCategoryDescription = "";
    }
}
