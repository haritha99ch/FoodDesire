namespace FoodDesire.IMS.ViewModels;

public partial class FoodItemsViewModel : ObservableRecipient, INavigationAware {
    private readonly IFoodItemsPageService _foodItemsPageService;
    private readonly IUserService<Chef> _chefService;
    private readonly IMapper _mapper;

    private FoodItemDetail? _selected;
    private Chef? _chef;

    [ObservableProperty]
    private bool _myFoodItemsOnly = false;

    public bool IsChef => App.CurrentUserAccount!.Role.Equals(Role.Chef);

    public FoodItemDetail? Selected {
        get => _selected;
        set {
            SetProperty(ref _selected, value);
            OnPropertyChanged(nameof(ItemSelected));
        }
    }

    public bool ItemSelected => Selected != null;

    public ObservableCollection<FoodItemDetail> FoodItems { get; private set; } = new();

    public FoodItemsViewModel(
        IFoodItemsPageService foodItemsPageService,
        IUserService<Chef> chefService,
        IMapper mapper
        ) {
        _foodItemsPageService = foodItemsPageService;
        _chefService = chefService;
        _mapper = mapper;
    }

    public async void OnNavigatedTo(object parameter) {
        if (IsChef) _chef = await _chefService.GetByEmail(App.CurrentUserAccount!.Email!);
        await GetFoodItems();
    }

    private async Task GetFoodItems() {
        FoodItems.Clear();
        List<FoodItem> foodItems = new();
        if (MyFoodItemsOnly) {
            foodItems = await _foodItemsPageService.GetAcceptedFoodItemsAsync(_chef!.Id) ?? new();
        } else {
            foodItems = await _foodItemsPageService.GetQueuedFoodItemsAsync() ?? new();
        }
        foodItems.ForEach(e => FoodItems.Add(_mapper.Map<FoodItemDetail>(e)));
    }

    public void OnNavigatedFrom() { }

    public void EnsureItemSelected() {
        Selected = FoodItems.FirstOrDefault();
    }

    [RelayCommand]
    private async Task UpdateFoodItems() {
        await GetFoodItems();
    }

    [RelayCommand]
    private async Task AddFoodItemToPreparingList() {
        FoodItem foodItem = await _foodItemsPageService.AddFootItemToPreparingList(Selected!.Id, _chef!.Id);
        if (!foodItem.Status.Equals(FoodItemStatus.Preparing)) return;
        await GetFoodItems();
    }

    [RelayCommand]
    private async Task MarkFoodItemAsPrepared() {
        FoodItem foodItem = await _foodItemsPageService.CompletePreparingFoodItem(Selected!.Id);
        if (!foodItem.Status.Equals(FoodItemStatus.Prepared)) return;
        await GetFoodItems();
    }
}
