using AutoMapper;
using FoodDesire.Models;

namespace FoodDesire.IMS.ViewModels;
public class IngredientsViewModel : ObservableRecipient, IInitializable {
    private readonly IIngredientsPageService _ingredientsPageService;
    private readonly IMapper _mapper;
    private bool _isLoading = true;
    private List<IngredientDetail> _ingredientsDetail = new();
    public List<IngredientDetail> IngredientsDetail {
        get => _ingredientsDetail;
        set => SetProperty(ref _ingredientsDetail, value);
    }
    public List<Ingredient> Ingredients { get; set; } = new();
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

    public IngredientsViewModel(IIngredientsPageService ingredientsPageService, IMapper mapper) {
        _mapper = mapper;
        _ingredientsPageService = ingredientsPageService;
        _ = OnInit();
    }

    public async Task OnInit() {
        Ingredients = await _ingredientsPageService.GetAllIngredients();
        IngredientsDetail = Ingredients.Select(_mapper.Map<IngredientDetail>).OrderBy(e => e.AvailableSpacePerCent).ToList();
        IsLoading = false;
    }
}
