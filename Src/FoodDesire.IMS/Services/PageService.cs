using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Concurrent;

namespace FoodDesire.IMS.Services;
public class PageService : IPageService {
    private readonly ConcurrentDictionary<string, Type> _pages = new ConcurrentDictionary<string, Type>();
    public PageService() {
        //Configure pages and view models here
    }

    public Type GetPageType(string pageKey) {
        if (_pages.TryGetValue(pageKey, out Type? pageType)) return pageType;
        throw new ArgumentException($"Page not found: {pageKey}. Configure page by calling Configure().");
    }

    private void Configure<VM, V>() where VM : ObservableObject where V : Page {
        string pageKey = nameof(VM);
        if (!_pages.TryAdd(pageKey, typeof(V))) throw new ArgumentException($"The key {pageKey} is already configured.");
    }
}
