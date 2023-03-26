using FoodDesire.IMS.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace FoodDesire.IMS.Views;

public sealed partial class SuppliesPage : Page
{
    public SuppliesViewModel ViewModel
    {
        get;
    }

    public SuppliesPage()
    {
        ViewModel = App.GetService<SuppliesViewModel>();
        InitializeComponent();
    }
}
