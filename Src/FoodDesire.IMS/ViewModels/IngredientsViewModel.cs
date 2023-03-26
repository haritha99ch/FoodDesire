﻿using AutoMapper;
using System.Collections.ObjectModel;

namespace FoodDesire.IMS.ViewModels;
public partial class IngredientsViewModel : ObservableRecipient, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly IMapper _mapper;
    [ObservableProperty]
    private bool _isLoading = true;
    [ObservableProperty]
    private ObservableCollection<IngredientDetails> _ingredientsDetail = new();

    public IngredientsViewModel(IIngredientsPageService ingredientsPageService, IMapper mapper) {
        _mapper = mapper;
        _ingredientsPageService = ingredientsPageService;
        _ = OnInit();
    }

    public async Task OnInit() {
        List<Ingredient> ingredients = await _ingredientsPageService.GetAllIngredients();
        List<IngredientDetails>? ingredientsDetails = ingredients
            .Select(_mapper.Map<IngredientDetails>)
            .OrderBy(e => e.AvailableSpacePerCent)
            .ToList();
        ingredientsDetails.ForEach(IngredientsDetail.Add);
        IsLoading = false;
    }

    public void NewIngredient(Ingredient ingredient) {
        IngredientsDetail.Insert(0, _mapper.Map<IngredientDetails>(ingredient));
    }

    public async Task DeleteIngredient(int ingredientId) {
        bool deleted = await _ingredientsPageService.DeleteIngredient(ingredientId);
        if (deleted) {
            IngredientDetails ingredientDetails = IngredientsDetail.SingleOrDefault(e => e.Id == ingredientId)!;
            IngredientsDetail.Remove(ingredientDetails);
        }
    }

    public async Task RequestIngredient(int ingredientId, double amount) {
        Supply requestedSupply = await _ingredientsPageService.RequestIngredient(ingredientId, amount);
        int listIndex = IngredientsDetail.IndexOf(IngredientsDetail.SingleOrDefault(e => e.Id == ingredientId)!);
        IngredientsDetail[listIndex] = _mapper.Map<IngredientDetails>(requestedSupply.Ingredient);
    }
}
