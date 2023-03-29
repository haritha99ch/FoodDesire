﻿namespace FoodDesire.IMS.Views;

public sealed partial class RecipesPage : Page {
    public RecipesViewModel ViewModel { get; }

    public RecipesPage() {
        ViewModel = App.GetService<RecipesViewModel>();
        InitializeComponent();
    }
}
