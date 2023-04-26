using System.Collections.ObjectModel;

namespace FoodDesire.Web.Client.Pages.RecipePages;
public partial class Index : ComponentBase {
    [Inject]
    private IRecipePageService _RecipePageService { get; set; } = default!;
    [Inject]
    private IComponentCommunicationService<string> _SearchCommunicationService { get; set; } = default!;
    [Inject]
    private IDialogService _dialogService { get; set; } = default!;

    private bool _loading = true;

    private string? Search { get; set; }
    private ObservableCollection<RecipeListItem> _recipes = new();


    protected override async Task OnInitializedAsync() {
        _SearchCommunicationService.OnChange += UpdateSearch;
        Search = _SearchCommunicationService.Value?.ToString();
        await UpdateRecipes();
        await base.OnInitializedAsync();
    }

    private async Task UpdateRecipes() {
        _loading = true;
        _recipes.Clear();
        await InvokeAsync(StateHasChanged);
        List<RecipeListItem> recipes = await _RecipePageService.GetRecipesBySearchAsync(Search) ?? new();
        recipes.ForEach(_recipes.Add);
        await InvokeAsync(StateHasChanged);
        _loading = false;
        await InvokeAsync(StateHasChanged);

    }

    private async void AddToCart(RecipeListItem recipe) {
        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, };
        DialogParameters? parameters = new() { [nameof(AddRecipeToCartDialogComponent.Recipe)] = recipe };
        DialogResult? result = await _dialogService.Show<AddRecipeToCartDialogComponent>("Add To Cart", parameters, maxWidth).Result;
    }

    public async void UpdateSearch(string? search) {
        Search = search;
        await UpdateRecipes();
    }

    private async void EditAndAddToCart(RecipeListItem recipe) {
        DialogOptions maxWidth = new DialogOptions() {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };
        DialogParameters? parameters = new() { [nameof(EditAndAddRecipeToCartDialogComponent.Recipe)] = recipe };
        DialogResult? result = await _dialogService.Show<EditAndAddRecipeToCartDialogComponent>("Edit And Add To Cart", parameters, maxWidth).Result;
    }
}
