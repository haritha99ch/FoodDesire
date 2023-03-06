using FoodDesire.Core.Contracts.Services;
using FoodDesire.IMS.Core.Models;

namespace FoodDesire.IMS.ViewModels;
public class HomeViewModel : ObservableRecipient {
    private readonly IIngredientService _ingredientService;

    private InventorySummary _inventorySummary;
    public InventorySummary InventorySummary {
        get => _inventorySummary;
        set => SetProperty(ref _inventorySummary, value);
    }

    public HomeViewModel(IIngredientService ingredientService) {
        _ingredientService = ingredientService;
        //LoadInventorySummaryAsync();
    }

    private async Task LoadInventorySummaryAsync() {
    }
}
