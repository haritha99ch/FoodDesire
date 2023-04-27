namespace FoodDesire.Web.Client.Components;
public partial class FoodItemListItemComponent : ComponentBase {
    [Parameter]
    public FoodItemListItem FoodItem { get; set; } = new();
    [Parameter]
    public bool ViewOnly { get; set; } = false;
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
