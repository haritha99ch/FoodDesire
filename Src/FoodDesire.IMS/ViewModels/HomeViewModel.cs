using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace FoodDesire.IMS.ViewModels;
public partial class HomeViewModel : ObservableRecipient, INavigationAware {
    private readonly IHomeService _homeService;
    private readonly INavigationService _navigationService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private bool _isLoading = true;
    [ObservableProperty]
    private bool _recipesLoading = true;
    [ObservableProperty]
    private bool _profitLoading = true;
    [ObservableProperty]
    private bool _chartLoading = true;
    [ObservableProperty]
    private bool _inventorySummeryLoading = true;
    [ObservableProperty]
    private decimal _income;
    [ObservableProperty]
    private decimal _expense;
    [ObservableProperty]
    private decimal _profit;
    [ObservableProperty]
    private float _profitPercentage;
    [ObservableProperty]
    private InventorySummary _inventorySummary = new();
    public ObservableCollection<RecipeListItemDetail> Recipes { get; set; } = new();
    [ObservableProperty]
    private int _pendingOrderCount;
    [ObservableProperty]
    private int _completedOrderCount;
    private List<Payment> _incomes = new();
    private List<Payment> _expenses = new();
    [ObservableProperty]
    public PlotModel _model = new();

    public HomeViewModel(IHomeService homeService, IMapper mapper, INavigationService navigationService) {
        _homeService = homeService;
        _mapper = mapper;
        _navigationService = navigationService;
    }

    public async void OnNavigatedTo(object parameter) {
        IsLoading = true;
        await loadFinancialData();
        await LoadToDosCount();
        await LoadTop10Recipes();
        await LoadInventorySummery();
        PlotChart();
        IsLoading = false;
    }

    private async Task LoadInventorySummery() {
        InventorySummary = await _homeService.GetInventorySummery();
        InventorySummeryLoading = false;
    }

    private async Task LoadToDosCount() {
        PendingOrderCount = await _homeService.GetPendingOrderCount();
        CompletedOrderCount = await _homeService.GetCompletedOrderCount();
    }

    private async Task LoadTop10Recipes() {
        List<Recipe> recipes = await _homeService.GetTop10Recipes();
        recipes.ForEach(e => Recipes.Add(_mapper.Map<RecipeListItemDetail>(e)));
        RecipesLoading = false;
    }

    private async Task loadFinancialData() {
        _incomes = await _homeService.GetIncomes();
        _expenses = await _homeService.GetExpenses();

        Income = _incomes.Sum(e => e.Value);
        Expense = _expenses.Sum(e => e.Value);

        Profit = Income - Expense;
        ProfitPercentage = (float)Math.Round((double)(Profit / Income) * 100, 2);
        ProfitLoading = false;
    }

    private void PlotChart() {
        LinearBarSeries income = new() {
            Title = "Income",
            FillColor = OxyColors.Lime
        };
        var groupedData = _incomes
            .GroupBy(e => e.DateTime.Date)
            .Select(g => new { Date = g.Key, Sum = g.Sum(e => e.Value) });
        foreach (var data in groupedData) {
            double dateAsDouble = data.Date.ToOADate();
            double valueAsDouble = (double)data.Sum;
            income.Points.Add(new DataPoint(dateAsDouble, valueAsDouble));
        }

        LinearBarSeries expenses = new() {
            Title = "Expense",
            FillColor = OxyColors.OrangeRed
        };
        groupedData = _expenses
            .GroupBy(e => e.DateTime.Date)
            .Select(g => new { Date = g.Key, Sum = g.Sum(e => e.Value) });
        foreach (var data in groupedData) {
            double dateAsDouble = data.Date.ToOADate();
            double valueAsDouble = (double)data.Sum;
            expenses.Points.Add(new DataPoint(dateAsDouble, valueAsDouble));
        }

        Model = new() {
            Title = "Income & Expenses",
            PlotAreaBorderColor = OxyColors.Transparent,
            Axes = {
                new DateTimeAxis{
                    Position = AxisPosition.Bottom,
                    StringFormat = "MM/dd/yyyy",
                    Title = "Date"
                },
                new LinearAxis{
                    Position = AxisPosition.Left,
                    Title = "Value"
                },
            },
            Series = { income, expenses }
        };
        ChartLoading = false;
    }

    public void OnNavigatedFrom() { }

    [RelayCommand]
    public void OnItemClick(RecipeListItemDetail? clickedItem) {
        if (clickedItem != null) {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RecipesDetailViewModel).FullName!, clickedItem.Id);
        }
    }
}


