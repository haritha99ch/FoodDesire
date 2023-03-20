using FoodDesire.Core.Contracts.Services;

namespace FoodDesire.IMS.ViewModels;
public class ShellViewModel : ObservableRecipient {
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService { get; }
    private readonly IAuthenticationService _authenticationService;
    private readonly ILocalSettingsService _localSettingsService;
    public INavigationViewService NavigationViewService { get; }

    public bool IsBackEnabled {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ShellViewModel(
        INavigationService navigationService,
        INavigationViewService navigationViewService,
        IAuthenticationService authenticationService,
        ILocalSettingsService localSettingsService
        ) {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _authenticationService = authenticationService;
        _localSettingsService = localSettingsService;
    }

    private void OnNavigated(object sender, NavigationEventArgs e) {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage)) {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null) {
            Selected = selectedItem;
        }
    }

    public async Task AuthenticateUser(string clientId) {
        App.CurrentUserAccount = await _authenticationService.AuthenticateUser(clientId);
        if (App.CurrentUserAccount == null) return;
        await _localSettingsService.SaveSettingAsync<Account>("CurrentUser", App.CurrentUserAccount);
    }
}
