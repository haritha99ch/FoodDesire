namespace FoodDesire.IMS.ViewModels;
public partial class RecipesDetailViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IUserService<Chef> _chefService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private RecipeDetail? _recipe;

    [ObservableProperty]
    private Chef _createdBy;

    public RecipesDetailViewModel(IRecipesPageService recipesPageService, IMapper mapper, IUserService<Chef> chefService) {
        _recipesPageService = recipesPageService;
        _chefService = chefService;
        _mapper = mapper;
    }

    public async void OnNavigatedTo(object parameter) {
        if (parameter is int recipeId) {
            Recipe = _mapper.Map<RecipeDetail>(await _recipesPageService.GetRecipeById(recipeId));
            CreatedBy = await _chefService.GetById(Recipe.ChefId);

            RecipeCategory recipeCategory = await _recipesPageService.GetRecipeCategoryById(Recipe.RecipeCategoryId);
            Recipe.CategoryName = recipeCategory.Name;
        }
    }

    public void OnNavigatedFrom() {
    }
}
