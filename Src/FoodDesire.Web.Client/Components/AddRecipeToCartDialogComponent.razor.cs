namespace FoodDesire.Web.Client.Components;
public partial class AddRecipeToCartDialogComponent : ComponentBase {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Inject] private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private IMapper _mapper { get; set; } = default!;
    [Inject] private ISnackbar _snackBar { get; set; } = default!;

    [Parameter]
    public RecipeListItem Recipe { get; set; } = default!;

    private List<FoodItemIngredientDetail> _foodItemIngredients { get; set; } = new();

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; } = default!;

    private bool _isAuthenticated = false;
    private RecipeDetail _recipe = default!;
    private int _quantity = 1;
    private bool Loading = true;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        Loading = true;
        _isAuthenticated = await _authenticationService.IsAuthenticated();
        _recipe = await _recipePageService.GetRecipeByIdAsync(Recipe.Id);
        AddIngredientsToFoodItem();
        Loading = false;
    }

    private void AddIngredientsToFoodItem() {
        _recipe.RecipeIngredients.ForEach(e => {
            _foodItemIngredients.Add(new() {
                Amount = e.Amount,
                CanModify = e.CanModify,
                Ingredient_Id = e.Ingredient_Id,
                Ingredient_Name = e.Ingredient_Name,
                Recipe_Id = e.Recipe_Id,
                Recipe_Name = e.Recipe_Name,
                IsRequired = e.IsRequired,
                Multiplier = (!e.IsRequired) ? 0 : 1,
                PricePerMultiplier = e.PricePerMultiplier,
                RecommendedMultiplier = e.RecommendedAmount / e.Amount,
                Measurement = e.Measurement,
            });
        });
    }


    private async void AddToCart() {
        if (!_isAuthenticated) return;
        Loading = true;
        Order order = await _recipePageService.GetCurrentUserExistingOrderAsync();
        Customer? customer = await _accountPageService.Get();
        if (customer == null) return;

        FoodItem? foodItem = new() {
            RecipeId = Recipe.Id,
            OrderId = order?.Id ?? 0,
            Order = (order == null) ? new() {
                CustomerId = customer.Id,
            } : null,
            Quantity = _quantity,
            FoodItemIngredients = _mapper.Map<List<FoodItemIngredient>>(_foodItemIngredients)
        };

        foodItem = await _recipePageService.AddFoodItemToCartAsync(foodItem);
        MudDialog.Close(DialogResult.Ok(true));
        OpenSnackBar();
    }

    private void OpenSnackBar() {
        _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
        _snackBar.Add("Added to the cart!", Severity.Success);
    }

    private void Cancel() {
        MudDialog.Close(DialogResult.Cancel);
    }
}
