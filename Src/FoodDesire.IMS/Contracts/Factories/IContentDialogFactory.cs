namespace FoodDesire.IMS.Contracts.Factories;
public interface IContentDialogFactory {
    T ConfigureDialog<T>(XamlRoot xamlRoot) where T : ContentDialog;
}
