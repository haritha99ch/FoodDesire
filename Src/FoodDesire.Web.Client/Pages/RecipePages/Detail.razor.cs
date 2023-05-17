using System.Collections.ObjectModel;

namespace FoodDesire.Web.Client.Pages.RecipePages;
public partial class Detail : ComponentBase {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Inject] private IDialogService _dialogService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IMapper _mapper { get; set; } = default!;

    [Parameter] public int RecipeId { get; set; }

    private bool _loading = true;
    private bool _isAuthenticated = false;
    private RecipeDetail? _recipeDetail;
    private ObservableCollection<RecipeReview> _recipeReviews = new();

    protected override async void OnInitialized() {
        base.OnInitialized();
        _loading = true;
        _isAuthenticated = await _authenticationService.IsAuthenticated();
        RecipeDetail recipe = await _recipePageService.GetRecipeByIdAsync(RecipeId);
        _recipeDetail = recipe;
        List<RecipeReview> recipeReviews = await _recipePageService.GetRecipeReviewsForRecipeAsync(RecipeId);
        recipeReviews.ForEach(_recipeReviews.Add);
        _loading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async void AddToCart() {
        if (!_isAuthenticated) NavigateToSignIn();
        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, };
        DialogParameters? parameters = new() { [nameof(AddRecipeToCartDialogComponent.Recipe)] = _mapper.Map<RecipeListItem>(_recipeDetail) };
        DialogResult? result = await _dialogService.Show<AddRecipeToCartDialogComponent>("Add To Cart", parameters, maxWidth).Result;
    }

    private async void EditAndAddToCart() {
        if (!_isAuthenticated) NavigateToSignIn();
        DialogOptions maxWidth = new DialogOptions() {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };
        DialogParameters? parameters = new() { [nameof(EditAndAddRecipeToCartDialogComponent.Recipe)] = _mapper.Map<RecipeListItem>(_recipeDetail) };
        DialogResult? result = await _dialogService.Show<EditAndAddRecipeToCartDialogComponent>("Edit And Add To Cart", parameters, maxWidth).Result;
    }

    private void NavigateToSignIn() {
        _navigationManager.NavigateTo("/Account/SignIn");
    }
}
