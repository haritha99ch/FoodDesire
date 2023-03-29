namespace FoodDesire.IMS.ViewModels;
public partial class RequestIngredientViewModel : ObservableObject, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;

    [ObservableProperty]
    private int _ingredientId;
    [ObservableProperty]
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
        _ = OnInit();
    }

    public async Task OnInit() {
        Ingredient ingredient = await _ingredientsPageService.GetIngredientById(IngredientId);
        IngredientDetails = App.GetService<IMapper>().Map<IngredientDetails>(ingredient);
        CurrentAmount = IngredientDetails.CurrentQuantity!;
    }
}
