namespace FoodDesire.Web.Client.Components;
public partial class FoodItemListItemComponent : ComponentBase {
    [Parameter]
    public FoodItemListDetail FoodItem { get; set; } = new();
    [Parameter]
    public EventCallback<int> OnDelete { get; set; }
    [Parameter]
    public EventCallback<int> OnEdit { get; set; }
    [Parameter]
    public EventCallback<int> OnView { get; set; }



    public async Task Delete() {
        await OnDelete.InvokeAsync(FoodItem.Id);
    }
    public async Task Edit() {
        await OnEdit.InvokeAsync(FoodItem.Id);
    }
    public async Task View() {
        await OnView.InvokeAsync(FoodItem.Id);
    }
}
