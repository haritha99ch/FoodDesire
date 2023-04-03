namespace FoodDesire.IMS.ViewModels;
public partial class RecipesDetailViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private RecipeListItemDetail? _recipe;

    public RecipesDetailViewModel(IRecipesPageService recipesPageService, IMapper mapper) {
        _recipesPageService = recipesPageService;
        _mapper = mapper;
    }

    public async void OnNavigatedTo(object parameter) {
        if (parameter is int recipeId) {
            Recipe = _mapper.Map<RecipeListItemDetail>(await _recipesPageService.GetRecipeById(recipeId));
        }
    }

    public void OnNavigatedFrom() {
    }
}
