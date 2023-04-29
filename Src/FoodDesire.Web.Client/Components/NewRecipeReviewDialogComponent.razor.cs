using Microsoft.AspNetCore.Components.Forms;

namespace FoodDesire.Web.Client.Components;
public partial class NewRecipeReviewDialogComponent : ComponentBase {
    [Inject] private IRecipePageService _recipePageService { get; set; } = default!;
    [Inject] private IAccountPageService _accountPageService { get; set; } = default!;

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public int RecipeId { get; set; }

    private bool _loading = true;
    private readonly RecipeReview _review = new();
    private int _rating;

    protected override async Task OnInitializedAsync() {
        _loading = true;
        await base.OnInitializedAsync();
        Customer customer = await _accountPageService.Get() ?? null!;
        if (customer == null) return;
        _review.RecipeId = RecipeId;
        _review.CustomerId = customer.Id;
        _loading = false;
    }

    private async void SubmitReview(EditContext context) {
        _review.Rating = _rating;
        await _recipePageService.AddRecipeReviewsForRecipeAsync(_review);
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() {
        MudDialog.Close(DialogResult.Cancel);
    }
}
