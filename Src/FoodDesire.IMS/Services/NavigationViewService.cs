using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FoodDesire.IMS.Services;
public class NavigationViewService : INavigationViewService {
    private readonly INavigationService _navigationService;
    private readonly IPageService _pageService;
    private NavigationView? _navigationView;
    public IList<object> MenuItems => _navigationView!.MenuItems;
    public object SettingsItem => _navigationView!.MenuItems;

    public NavigationViewService(INavigationService navigationService, IPageService pageService) {
        _navigationService = navigationService;
        _pageService = pageService;
    }

    [MemberNotNull(nameof(_navigationView))]
    public void Initialize(NavigationView navigationView) {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    public void UnregisterEvents() {
        if (_navigationView == null) return;
        _navigationView.BackRequested -= OnBackRequested;
        _navigationView.ItemInvoked -= OnItemInvoked;
    }

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();

    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
        if (args.IsSettingsInvoked) {
            //_navigationService.NavigateTo(typeof(SettingsViewModel).FullName!); //TODO: After implementing settings page
            return;
        }
        NavigationViewItem? selectedItem = args.InvokedItem as NavigationViewItem;
        if (selectedItem!.GetValue(NavigationHelper.NavigateToProperty) is string pageKey) _navigationService.NavigateTo(pageKey);
    }

    private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type pageType) {
        foreach (NavigationViewItem item in menuItems.OfType<NavigationViewItem>()) {
            if (IsMenuItemForPageType(item, pageType)) return item;
            NavigationViewItem? selectedChild = GetSelectedItem(item.MenuItems, pageType);
            if (selectedChild != null) return selectedChild;
        }
        return null;
    }

    public NavigationViewItem? GetSelectedItem(Type pageType) {
        if (_navigationView == null) return null;
        return GetSelectedItem(_navigationView.MenuItems, pageType) ?? GetSelectedItem(_navigationView.FooterMenuItems, pageType);
    }

    private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType) {
        if (menuItem.GetValue(NavigationHelper.NavigateToProperty) is not string pageKey) return false;
        return _pageService.GetPageType(pageKey) == sourcePageType;
    }
}
