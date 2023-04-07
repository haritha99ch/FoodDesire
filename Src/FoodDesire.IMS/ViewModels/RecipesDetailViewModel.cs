using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipesDetailViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IUserService<Chef> _chefService;
    private readonly INavigationService _navigationService;
    private readonly IMapper _mapper;

    public bool IsChef => App.CurrentUserAccount?.Role == Role.Chef;

    [ObservableProperty]
    private RecipeDetail? _recipe;

    [ObservableProperty]
    private Chef? _createdBy;

    public RecipesDetailViewModel(
        IRecipesPageService recipesPageService,
        IMapper mapper,
        IUserService<Chef> chefService,
        INavigationService navigationService
        ) {
        _recipesPageService = recipesPageService;
        _chefService = chefService;
        _mapper = mapper;
        _navigationService = navigationService;
    }

    public async void OnNavigatedTo(object parameter) {
        if (parameter is int recipeId) {
            Recipe = _mapper.Map<RecipeDetail>(await _recipesPageService.GetRecipeById(recipeId));
            CreatedBy = await _chefService.GetById(Recipe.ChefId);

            RecipeCategory recipeCategory = await _recipesPageService.GetRecipeCategoryById(Recipe.RecipeCategoryId);
            Recipe.CategoryName = recipeCategory.Name;
        }
    }

    public void OnNavigatedFrom() { }

    [RelayCommand]
    public void EditRecipe() {
        _navigationService.SetListDataItemForNextConnectedAnimation(Recipe!);
        _navigationService.NavigateTo(typeof(EditRecipeViewModel).FullName!, Recipe!.Id);
    }

    [RelayCommand]
    private async void DeleteRecipe(XamlRoot xamlRoot) {
        ContentDialog dialog = new() {
            XamlRoot = xamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Close,
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            Title = "Delete Recipe",
            Content = $"Are you sure you want to delete this recipe?"
        };
        ContentDialogResult result = await dialog.ShowAsync();

        if (!result.Equals(ContentDialogResult.Primary)) return;
        bool deleted = await _recipesPageService.DeleteRecipeById(Recipe!.Id);
        if (deleted) _navigationService.GoBack();
    }
}
