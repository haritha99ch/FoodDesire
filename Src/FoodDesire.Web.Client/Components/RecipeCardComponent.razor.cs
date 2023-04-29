using System.ComponentModel.DataAnnotations;

namespace FoodDesire.Web.Client.Components;
public partial class RecipeCardComponent : ComponentBase {

    [Parameter]
    public RecipeListItem? Recipe { get; set; }
    [Parameter]
    public Action<RecipeListItem> AddToCartAction { get; set; } = default!;
    [Parameter]
    public Action<RecipeListItem> EditAndAddToCartAction { get; set; } = default!;
    [Parameter]
    public Action<RecipeListItem> ShowDetailAction { get; set; } = default!;

    private void AddToCart() {
        AddToCartAction(Recipe!);
    }

    private void EditAndAddToCart() {
        EditAndAddToCartAction(Recipe!);
    }

    private void ShowDetail() {
        ShowDetailAction(Recipe!);
    }
}

public enum RecipeAction {
    [Display(Name = "Add To Cart")]
    AddToCart,
    [Display(Name = "Edit & Add To Cart")]
    EditAndAddToCart
}
