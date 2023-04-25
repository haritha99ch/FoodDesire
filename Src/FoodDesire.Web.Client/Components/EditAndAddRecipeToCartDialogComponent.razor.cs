using AutoMapper;
using MudBlazor;

namespace FoodDesire.Web.Client.Components;
public partial class EditAndAddRecipeToCartDialogComponent {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Inject] private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private IMapper _mapper { get; set; } = default!;
    [Parameter]
    public RecipeListItem Recipe { get; set; } = default!;
    private List<FoodItemIngredientDetail> _foodItems { get; set; } = new();
    private FoodItem? _foodItem { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; } = default!;

    private bool _isAuthenticated = false;
    private RecipeDetail _recipe = default!;
    private int _quantity = 1;
    private bool _loading = true;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        _loading = true;
        _isAuthenticated = await _authenticationService.IsAuthenticated();
        _recipe = await _recipePageService.GetRecipeByIdAsync(Recipe.Id);
        AddIngredientsToFoodItem();
        _foodItem = new() {
            RecipeId = _recipe.Id,
            Quantity = _quantity,
            Price = _recipe.FixedPrice
        };
        _loading = false;
    }

    private void AddIngredientsToFoodItem() {
        _recipe.RecipeIngredients.ForEach(e => {
            _foodItems.Add(new() {
                Amount = e.Amount,
                CanModify = e.CanModify,
                Ingredient_Id = e.Ingredient_Id,
                Ingredient_Name = e.Ingredient_Name,
                Recipe_Id = e.Recipe_Id,
                Recipe_Name = e.Recipe_Name,
                IsRequired = e.IsRequired,
                Multiplier = 1,
                PricePerMultiplier = e.PricePerMultiplier,
                RecommendedMultiplier = e.RecommendedAmount / e.Amount
            });
        });
    }

    private async void AddToCart() {
        if (!_isAuthenticated) return;
        _loading = true;
        Customer? customer = await _accountPageService.Get();
        if (customer == null) return;
        Order? order = await _recipePageService.GetCurrentUserExistingOrderAsync();

        _foodItem!.FoodItemIngredients = _mapper.Map<List<FoodItemIngredient>>(_foodItems);
        _foodItem!.OrderId = order?.Id;
        _foodItem!.Order = (order == null) ? new() { CustomerId = customer.Id } : null;

        _foodItem = await _recipePageService.AddFoodItemToCartAsync(_foodItem);
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() {
        MudDialog.Close(DialogResult.Cancel);
    }
}
