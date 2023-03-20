namespace FoodDesire.IMS.ViewModels;
public partial class ShellViewModel : ObservableRecipient {
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService { get; }
    private readonly IAuthenticationService _authenticationService;
    private readonly ILocalSettingsService _localSettingsService;
    public INavigationViewService NavigationViewService { get; }

    [ObservableProperty]
    private User _user;

    public bool IsBackEnabled {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    [ObservableProperty]
    private Admin? _admin;
    [ObservableProperty]
    private Chef? _chef;
    [ObservableProperty]
    private Supplier? _supplier;
    [ObservableProperty]
    private Deliverer? _deliverer;

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
        await _localSettingsService.SaveSettingAsync<string>("CurrentUser", App.CurrentUserAccount.Email);
    }


    public async Task GetUser() {
        if (App.CurrentUserAccount == null) return;
        switch (App.CurrentUserAccount.Role) {
            case Role.Admin:
                Admin = await App.GetService<IUserService<Admin>>().GetByEmail(App.CurrentUserAccount.Email);
                break;
            case Role.Chef:
                Chef = await App.GetService<IUserService<Chef>>().GetByEmail(App.CurrentUserAccount.Email);
                break;
            case Role.Deliverer:
                Deliverer = await App.GetService<IUserService<Deliverer>>().GetByEmail(App.CurrentUserAccount.Email);
                break;
            case Role.Supplier:
                Supplier = await App.GetService<IUserService<Supplier>>().GetByEmail(App.CurrentUserAccount.Email);
                break;
        }

        User = App.CurrentUserAccount.User!;
    }
}
