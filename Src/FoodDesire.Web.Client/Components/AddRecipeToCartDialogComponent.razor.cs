using MudBlazor;

namespace FoodDesire.Web.Client.Components;
public partial class AddRecipeToCartDialogComponent : ComponentBase {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Inject] private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Parameter]
    public RecipeListItem Recipe { get; set; } = default!;

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
        Loading = false;
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
            Quantity = _quantity
        };

        foodItem = await _recipePageService.AddFoodItemToCartAsync(foodItem);

        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() {
        MudDialog.Close(DialogResult.Cancel);
    }
}
