namespace FoodDesire.Web.Client.Shared;
public partial class NavigationBarComponent : ComponentBase {
    [Inject]
    private NavigationManager? _navigationManager { get; set; }
    [Inject]
    private IComponentCommunicationService<string>? _searchCommunicationService { get; set; }
    private string? Search { get; set; }
    private bool open = false;

    void ToggleDrawer() {
        open = !open;
    }

    private void NavigateTo(string pageUri) {
        open = false;
        _navigationManager!.NavigateTo(pageUri);
    }

    private void OnSearchTextChanged() {
        if (!_navigationManager!.Uri.Contains("/Recipe/Index")) {
            NavigateTo("/Recipe/Index");
        }
        _searchCommunicationService!.NotifyStateChanged(Search!);
    }
}
