﻿using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipesViewModel : ObservableRecipient, INavigationAware {
    private readonly INavigationService _navigationService;
    private readonly IRecipesPageService _recipesPageService;
    private readonly IMapper _mapper;

    public ObservableCollection<RecipeDetail> Recipes { get; } = new();
    public IRelayCommand<RecipeDetail> OnItemClickedCommand { get; }

    public RecipesViewModel(INavigationService navigationService, IRecipesPageService recipesPageService, IMapper mapper) {
        _navigationService = navigationService;
        _recipesPageService = recipesPageService;
        _mapper = mapper;
        OnItemClickedCommand = new RelayCommand<RecipeDetail>(OnItemClick);
    }

    public async void OnNavigatedTo(object parameter) {
        Recipes.Clear();

        List<Recipe> recipes = await _recipesPageService.GetAllRecipes();
        recipes.ForEach(e => Recipes.Add(_mapper.Map<RecipeDetail>(e)));
    }

    public void OnNavigatedFrom() {
    }

    public void OnItemClick(RecipeDetail? clickedItem) {
        if (clickedItem != null) {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RecipesDetailViewModel).FullName!, clickedItem.Id);
        }
    }
}
