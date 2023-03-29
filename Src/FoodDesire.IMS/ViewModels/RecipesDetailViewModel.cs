using CommunityToolkit.Mvvm.ComponentModel;

using FoodDesire.IMS.Contracts.ViewModels;
using FoodDesire.IMS.Core.Contracts.Services;
using FoodDesire.IMS.Core.Models;

namespace FoodDesire.IMS.ViewModels;

public class RecipesDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private SampleOrder? _item;

    public SampleOrder? Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    public RecipesDetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is long orderID)
        {
            var data = await _sampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
