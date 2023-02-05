﻿namespace FoodDesire.Core.Test;
[TestFixture]
public class IngredientServices {
    private readonly ISupplierService _supplierService;
    private readonly IIngredientService _ingredientService;
    private readonly FoodDesireContext _context;

    public IngredientServices() {
        ApplicationHostHelper.ConfigureHost("IngredientServices");

        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _supplierService = ApplicationHostHelper.GetService<ISupplierService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task SetUp() {
        await _context.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewIngredientCategory() {
        IngredientCategory? ingredientCategory = await _ingredientService.NewIngredientCategory(new IngredientCategory() {
            Name = "Key",
            Description = "Key Ingredients",
        });
        Assert.That(ingredientCategory.Name, Is.EqualTo("Key"));
    }

    [Test, Order(2)]
    public async Task NewIngredient() {
        IngredientCategory? ingredientCategory = await _ingredientService.GetIngredientCategoryByName("Key");
        Ingredient newIngredient = new Ingredient() {
            Name = "Salt",
            Description = "Salt",
            IngredientCategoryId = ingredientCategory.Id,
            CurrentPricePerUnit = 0,
            CurrentQuantity = 0,
            Image = new Image() { Data = "Image data" },
            MaximumQuantity = 1000,
            Measurement = Measurement.Mass
        };
        Ingredient savedIngredient = await _ingredientService.NewIngredient(newIngredient);
        Assert.That(savedIngredient.Name, Is.EqualTo(newIngredient.Name));
    }
}