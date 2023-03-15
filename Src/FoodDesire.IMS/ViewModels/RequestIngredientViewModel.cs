using AutoMapper;

namespace FoodDesire.IMS.ViewModels;
public partial class RequestIngredientViewModel : ObservableObject, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;

    [ObservableProperty]
    private int _ingredientId;
    [ObservableProperty]
    private IngredientDetails _ingredientDetails;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanRequest))]
    [NotifyPropertyChangedFor(nameof(RequestingAmount))]
    private double currentAmount;
    public double RequestingAmount => CurrentAmount - IngredientDetails.CurrentQuantity;
    public bool CanRequest => CurrentAmount != IngredientDetails.CurrentQuantity;

    public RequestIngredientViewModel(int ingredientId) {
        _ingredientsPageService = App.GetService<IIngredientsPageService>();
        _ingredientId = ingredientId;
        _ingredientDetails = new();
        OnInit();
    }

    public async void OnInit() {
        Ingredient ingredient = await _ingredientsPageService.GetIngredientById(IngredientId);
        IngredientDetails = App.GetService<IMapper>().Map<IngredientDetails>(ingredient);
        currentAmount = IngredientDetails.CurrentQuantity;
    }
}
