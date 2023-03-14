using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace FoodDesire.IMS.ViewModels;
public partial class IngredientsViewModel : ObservableRecipient, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly IMapper _mapper;
    [ObservableProperty]
    private bool _isLoading = true;
    private List<IngredientDetails> _ingredientsDetail = new();
    public List<IngredientDetails> IngredientsDetail {
        get => _ingredientsDetail;
        set => SetProperty(ref _ingredientsDetail, value);
    }
    public List<Ingredient> Ingredients { get; set; } = new();

    public IngredientsViewModel(IIngredientsPageService ingredientsPageService, IMapper mapper) {
        _mapper = mapper;
        _ingredientsPageService = ingredientsPageService;
        _ = OnInit();
    }

    public async Task OnInit() {
        Ingredients = await _ingredientsPageService.GetAllIngredients();
        IngredientsDetail = Ingredients
            .Select(_mapper.Map<IngredientDetails>)
            .OrderBy(e => e.AvailableSpacePerCent)
            .ToList();
        IsLoading = false;
    }

    [RelayCommand]
    public void OnItemClick(IngredientDetails ingredientDetails) {

        Debug.WriteLine($"Selected item: {ingredientDetails}");
    }
}
