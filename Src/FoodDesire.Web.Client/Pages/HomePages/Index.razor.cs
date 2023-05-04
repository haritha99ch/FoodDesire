namespace FoodDesire.Web.Client.Pages.HomePages;
public partial class Index : ComponentBase {
    [Inject] private IHomePageService _homePageService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IDialogService _dialogService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;

    private bool _loading = true;
    private bool _isAuthenticated = false;
    private List<RecipeListItem> _recipes = new();

    protected override async Task OnInitializedAsync() {
        _loading = true;
        await base.OnInitializedAsync();
        _isAuthenticated = await _authenticationService.IsAuthenticated();
        _recipes = await _homePageService.GetTop10Recipes();
        _loading = false;
    }

    private async void AddToCart(RecipeListItem recipe) {
        if (!_isAuthenticated) NavigateToSignIn();
        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, };
        DialogParameters? parameters = new() { [nameof(AddRecipeToCartDialogComponent.Recipe)] = recipe };
        DialogResult? result = await _dialogService.Show<AddRecipeToCartDialogComponent>("Add To Cart", parameters, maxWidth).Result;
    }
    private async void EditAndAddToCart(RecipeListItem recipe) {
        if (!_isAuthenticated) NavigateToSignIn();
        DialogOptions maxWidth = new DialogOptions() {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };
        DialogParameters? parameters = new() { [nameof(EditAndAddRecipeToCartDialogComponent.Recipe)] = recipe };
        DialogResult? result = await _dialogService.Show<EditAndAddRecipeToCartDialogComponent>("Edit And Add To Cart", parameters, maxWidth).Result;
    }

    private void ShowDetail(RecipeListItem recipe) {
        _navigationManager.NavigateTo($"/Recipe/{recipe.Id}");
    }

    private void NavigateToSignIn() {
        _navigationManager.NavigateTo("/Account/SignIn");
    }
}
