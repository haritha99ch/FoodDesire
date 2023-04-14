namespace FoodDesire.Web.Client.Pages.RecipePages;
public partial class Index : ComponentBase {
    [Inject]
    private IComponentCommunicationService<string>? _SearchCommunicationService { get; set; }

    private string? Search { get; set; }

    protected override void OnInitialized() {
        _SearchCommunicationService!.OnChange += UpdateSearch;
        Search = _SearchCommunicationService.Value.ToString();
    }

    public void UpdateSearch(string? search) {
        Search = search;
    }
}
