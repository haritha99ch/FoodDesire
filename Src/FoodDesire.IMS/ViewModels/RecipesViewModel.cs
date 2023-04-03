using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipesViewModel : ObservableRecipient, INavigationAware {
    private readonly INavigationService _navigationService;
    private readonly IRecipesPageService _recipesPageService;
    private readonly IMapper _mapper;

    public ObservableCollection<RecipeListItemDetail> Recipes { get; } = new();
    public IRelayCommand<RecipeListItemDetail> OnItemClickedCommand { get; }

    public RecipesViewModel(INavigationService navigationService, IRecipesPageService recipesPageService, IMapper mapper) {
        _navigationService = navigationService;
        _recipesPageService = recipesPageService;
        _mapper = mapper;
        OnItemClickedCommand = new RelayCommand<RecipeListItemDetail>(OnItemClick);
    }

    public async void OnNavigatedTo(object parameter) {
        Recipes.Clear();

        List<Recipe> recipes = await _recipesPageService.GetAllRecipes();
        recipes.ForEach(e => Recipes.Add(_mapper.Map<RecipeListItemDetail>(e)));
    }

    public void OnNavigatedFrom() {
    }

    public void OnItemClick(RecipeListItemDetail? clickedItem) {
        if (clickedItem != null) {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RecipesDetailViewModel).FullName!, clickedItem.Id);
        }
    }

    [RelayCommand]
    public void AddNewRecipe(XamlRoot xamlRoot) {
        _navigationService.NavigateTo(typeof(NewRecipeViewModel).FullName!);
    }
}
