using MudBlazor;

namespace FoodDesire.Web.Client.Components;
public partial class DeleteConfirmationDialogComponent {
    [CascadingParameter]
    MudDialogInstance? MudDialog { get; set; }

    void Submit() => MudDialog?.Close(DialogResult.Ok(true));
    void Cancel() => MudDialog?.Cancel();
}
