using FoodDesire.IMS.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace FoodDesire.IMS.Views;

public sealed partial class EmployeesPage : Page
{
    public EmployeesViewModel ViewModel
    {
        get;
    }

    public EmployeesPage()
    {
        ViewModel = App.GetService<EmployeesViewModel>();
        InitializeComponent();
    }
}
