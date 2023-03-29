namespace FoodDesire.IMS.ViewModels;
public partial class RequestIngredientViewModel : ObservableRecipient {
    private readonly IIngredientsPageService _ingredientsPageService;

    [ObservableProperty]
    private int _ingredientId;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanRequest))]
    [NotifyPropertyChangedFor(nameof(RequestingAmount))]
    private IngredientDetails? _ingredientDetails;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanRequest))]
    [NotifyPropertyChangedFor(nameof(RequestingAmount))]
    private double _currentAmount = 0;
    public double RequestingAmount {
        get => CurrentAmount - IngredientDetails!.CurrentQuantity;
        set => CurrentAmount = IngredientDetails!.CurrentQuantity + value;
    }
    public bool CanRequest => CurrentAmount != IngredientDetails!.CurrentQuantity;

    public RequestIngredientViewModel(int ingredientId) {
        _ingredientsPageService = App.GetService<IIngredientsPageService>();
        IngredientId = ingredientId;
        IngredientDetails = new();
        OnInit();
    }

    public async void OnInit() {
        Ingredient ingredient = await _ingredientsPageService.GetIngredientById(IngredientId);
        IngredientDetails = App.GetService<IMapper>().Map<IngredientDetails>(ingredient);
        CurrentAmount = IngredientDetails.CurrentQuantity!;
    }
}
