using System.Diagnostics;

namespace FoodDesire.Web.Client.Pages.RecipePages;
public partial class Index : ComponentBase {
    [Inject]
    private IRecipePageService _RecipePageService { get; set; } = default!;
    [Inject]
    private IComponentCommunicationService<string> _SearchCommunicationService { get; set; } = default!;

    private List<RecipeListItem> _recipes = new();

    private string? Search { get; set; }

    protected override async Task OnInitializedAsync() {
        _SearchCommunicationService.OnChange += UpdateSearch;
        Search = _SearchCommunicationService.Value?.ToString();
        await UpdateRecipes();
        await base.OnInitializedAsync();
    }


    private async Task UpdateRecipes() {
        _recipes.Clear();
        _recipes = await _RecipePageService.GetRecipesBySearchAsync(Search) ?? new();
    }

    public async void UpdateSearch(string? search) {
        Search = search;
        await UpdateRecipes();
    }

    private void AddToCart(RecipeListItem recipe) {
        Debug.WriteLine($"Recipe {recipe.Name} was clicked.");
    }

    private void EditAndAddToCart(RecipeListItem recipe) {
        Debug.WriteLine($"Recipe {recipe.Name} was clicked.");
    }
}
