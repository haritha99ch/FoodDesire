﻿namespace FoodDesire.IMS.Factories;
public class ContentDialogFactory : IContentDialogFactory {
    public T ConfigureDialog<T>(XamlRoot xamlRoot) where T : ContentDialog {
        T dialog = App.GetService<T>();
        dialog.XamlRoot = xamlRoot;
        dialog.Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"];
        return dialog;
    }
}