using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipesDetailViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IUserService<Chef> _chefService;
    private readonly INavigationService _navigationService;
    private readonly IMapper _mapper;

    public bool IsChef => App.CurrentUserAccount?.Role == Role.Chef;

    [ObservableProperty]
    private Recipe _recipeNP = default!;
    [ObservableProperty]
    private RecipeDetail? _recipe;

    [ObservableProperty]
    private Chef? _createdBy;
    [ObservableProperty]
    private bool _loading = true;

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
            RecipeNP = await _recipesPageService.GetRecipeById(recipeId);
            Recipe = _mapper.Map<RecipeDetail>(RecipeNP);
            CreatedBy = RecipeNP.Chef;
            Recipe.CategoryName = RecipeNP.RecipeCategory?.Name;
            Loading = false;
        }
    }

    public void OnNavigatedFrom() { }

    [RelayCommand]
    public void EditRecipe() {
        _navigationService.SetListDataItemForNextConnectedAnimation(Recipe!);
        _navigationService.NavigateTo(typeof(EditRecipeViewModel).FullName!, RecipeNP);
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
