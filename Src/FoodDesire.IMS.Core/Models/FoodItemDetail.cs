namespace FoodDesire.IMS.Core.Models;
public partial class FoodItemDetail : ObservableObject {
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private Recipe _recipe = default!;
    [ObservableProperty]
    private int _quantity;
    [ObservableProperty]
    private DateTime _dateTime;
    ObservableCollection<FoodItemIngredient> _foodItemIngredients = new();
    [ObservableProperty]
    private FoodItemStatus _status;
    [ObservableProperty]
    private Chef? _chef;
    [ObservableProperty]
    private Order? _order;

    public bool IsPreparing => Status.Equals(FoodItemStatus.Preparing);
    public string? PreparedBy => $"{Chef?.User?.FirstName} {Chef?.User?.LastName}";
}
