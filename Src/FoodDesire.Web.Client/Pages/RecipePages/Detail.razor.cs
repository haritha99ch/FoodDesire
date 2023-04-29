namespace FoodDesire.Web.Client.Pages.RecipePages;
public partial class Detail : ComponentBase {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Parameter] public int RecipeId { get; set; }

    private bool _loading = true;
    private RecipeDetail? _recipeDetail;

    protected override async void OnInitialized() {
        base.OnInitialized();
        _loading = true;
        RecipeDetail recipe = await _recipePageService.GetRecipeByIdAsync(RecipeId);
        _recipeDetail = recipe;
        _loading = false;
        await InvokeAsync(StateHasChanged);
    }
}
