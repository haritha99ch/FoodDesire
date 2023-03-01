namespace FoodDesire.IMS.ViewModels;
public class ShellViewModel : ObservableRecipient {
    private object? _selected;
    private bool _isBackEnabled;
    public INavigationViewService NavigationViewService { get; }
    public INavigationService NavigationService { get; }
    public bool IsBackEnabled {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }
    public object? Selected {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ShellViewModel(
        INavigationViewService navigationViewService,
        INavigationService navigationService
        ) {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
    }

    private void OnNavigated(object sender, NavigationEventArgs e) {
        IsBackEnabled = NavigationService.CanGoBack;
        //if(e.SourcePageType == typeof(SettingsPage)) {
        //    Selected = NavigationViewService.SettingsItem; //TODO: Settings page
        //    return;
        //}
        NavigationViewItem? selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null) Selected = selectedItem;
    }

}
