namespace FoodDesire.Web.Client.Shared;
public partial class NavigationBarComponent : ComponentBase {
    [Inject]
    private NavigationManager? _navigationManager { get; set; }
    [Inject]
    private IComponentCommunicationService<string>? _searchCommunicationService { get; set; }
    private string? Search { get; set; }

    private void NavigateTo(string pageKey) {
        _navigationManager!.NavigateTo(pageKey);
    }

    private void OnSearchTextChanged() {
        if (!_navigationManager!.Uri.Contains("/Recipe/Index")) {
            _navigationManager.NavigateTo("/Recipe/Index");
        }
        _searchCommunicationService!.NotifyStateChanged(Search!);
    }
}
