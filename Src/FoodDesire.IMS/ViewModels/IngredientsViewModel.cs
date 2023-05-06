namespace FoodDesire.IMS.ViewModels;
public partial class IngredientsViewModel : ObservableRecipient, INavigationAware {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly IMapper _mapper;
    [ObservableProperty]
    private bool _isLoading = true;
    [ObservableProperty]
    private ObservableCollection<IngredientDetails> _ingredientsDetail = new();
    private string? _searchText;
    public string? SearchText {
        get => _searchText;
        set {
            OnSearchTextChanged();
            SetProperty(ref _searchText, value);
        }
    }
    private readonly SemaphoreSlim _searchSemaphore = new(1, 1);
    public bool IngredientControlAccess => App.CurrentUserAccount!.Role.Equals(Role.Admin) || App.CurrentUserAccount!.Role.Equals(Role.Chef);

    public IngredientsViewModel(IIngredientsPageService ingredientsPageService, IMapper mapper) {
        _mapper = mapper;
        _ingredientsPageService = ingredientsPageService;
    }

    public async void OnNavigatedTo(object parameter) {
        await LoadData();
    }

    private async Task LoadData() {
        IsLoading = true;
        List<Ingredient> ingredients = await _ingredientsPageService.GetAllIngredients();
        List<IngredientDetails>? ingredientsDetails = ingredients
            .Select(_mapper.Map<IngredientDetails>)
            .OrderBy(e => e.AvailableSpacePerCent)
            .ToList();
        ingredientsDetails.ForEach(IngredientsDetail.Add);
        IsLoading = false;
    }

    public void OnNavigatedFrom() { }

    private async void OnSearchTextChanged() {
        await _searchSemaphore.WaitAsync();
        try {
            IsLoading = true;
            if (string.IsNullOrEmpty(SearchText) || string.IsNullOrWhiteSpace(SearchText)) {
                IngredientsDetail.Clear();
                List<Ingredient> ingredients = await _ingredientsPageService.GetAllIngredients();
                List<IngredientDetails>? ingredientsDetails = ingredients
                    .Select(_mapper.Map<IngredientDetails>)
                    .OrderBy(e => e.AvailableSpacePerCent)
                    .ToList();
                ingredientsDetails.ForEach(IngredientsDetail.Add);
            } else {
                IngredientsDetail.Clear();
                List<Ingredient> ingredients = await _ingredientsPageService.SearchIngredients(SearchText);
                List<IngredientDetails>? ingredientsDetails = ingredients
                    .Select(_mapper.Map<IngredientDetails>)
                    .OrderBy(e => e.AvailableSpacePerCent)
                    .ToList();
                ingredientsDetails.ForEach(IngredientsDetail.Add);
            }
            IsLoading = false;
        } finally {
            _searchSemaphore.Release();
        }
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
