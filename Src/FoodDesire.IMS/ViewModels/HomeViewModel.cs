namespace FoodDesire.IMS.ViewModels;
public partial class HomeViewModel : ObservableRecipient, INavigationAware {
    private readonly IHomeService _homeService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private bool _isLoading;
    [ObservableProperty]
    private decimal _income;
    [ObservableProperty]
    private decimal _expense;
    [ObservableProperty]
    private decimal _profit;
    [ObservableProperty]
    private float _profitPercentage;
    public ObservableCollection<RecipeListItemDetail> Recipes { get; set; } = new();
    [ObservableProperty]
    private int _pendingOrderCount;
    [ObservableProperty]
    private int _completedOrderCount;

    public HomeViewModel(IHomeService homeService, IMapper mapper) {
        _homeService = homeService;
        _mapper = mapper;
    }

    public async void OnNavigatedTo(object parameter) {
        IsLoading = true;

        Income = await _homeService.GetTotalIncome();
        Expense = await _homeService.GetTotalExpense();

        Profit = Income - Expense;
        ProfitPercentage = (float)Math.Round((double)(Profit / Income) * 100, 2);

        PendingOrderCount = await _homeService.GetPendingOrderCount();
        CompletedOrderCount = await _homeService.GetCompletedOrderCount();

        List<Recipe> recipes = await _homeService.GetTop10Recipes();
        recipes.ForEach(e => Recipes.Add(_mapper.Map<RecipeListItemDetail>(e)));

        IsLoading = false;
    }

    public void OnNavigatedFrom() { }
}
