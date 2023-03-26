namespace FoodDesire.IMS.Services;
public class NavigationService : INavigationService {
    private readonly IPageService _pageService;
    private object? _lastParameterUsed;
    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame != null && Frame.CanGoBack;
    private Frame? _frame;
    public Frame? Frame {
        get {
            if (_frame != null) return _frame;
            _frame = App.MainWindow.Content as Frame;
            RegisterFrameEvents();
            return _frame!;
        }
        set {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }
    public event NavigatedEventHandler? Navigated;

    public NavigationService(IPageService pageService) {
        _pageService = pageService;
    }

    private void RegisterFrameEvents() {
        if (_frame != null) _frame.Navigated += OnNavigated;
    }

    private void UnregisterFrameEvents() {
        if (_frame != null) _frame.Navigated -= OnNavigated;
    }

    private void OnNavigated(object sender, NavigationEventArgs e) {
        if (sender is not Frame frame) return;
        bool clearNavigation = (bool)frame.Tag;

        if (clearNavigation) frame.BackStack.Clear();
        if (frame.GetPageViewModel() is INavigationAware navigationAware) {
            navigationAware.OnNavigatedTo(e.Parameter);
        }
        Navigated?.Invoke(sender, e);
    }

    public bool GoBack() {
        if (!CanGoBack) return false;
        object? vmBeforeNavigation = null;
        _frame.GoBack();
        if ((vmBeforeNavigation is INavigationAware navigationAware)) navigationAware.OnNavigatedFrom();
        return true;
    }

    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false) {
        Type? pageType = _pageService.GetPageType(pageKey);

        if (!(_frame != null && (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))) return false;
        _frame.Tag = clearNavigation;
        object? vmBeforeNavigation = _frame.GetPageViewModel();
        bool navigated = _frame.Navigate(pageType, parameter);

        if (!navigated) return navigated;
        _lastParameterUsed = parameter;
        if ((vmBeforeNavigation is INavigationAware navigationAware)) navigationAware.OnNavigatedFrom();
        return navigated;
    }
}
