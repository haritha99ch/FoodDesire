namespace FoodDesire.Web.Client.Pages;
public partial class Index : ComponentBase {
    [Inject] private IHomePageService _homePageService { get; set; } = default!;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        List<Recipe> recipes = await _homePageService.GetTop10Recipes();
    }
}
