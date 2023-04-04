namespace FoodDesire.IMS.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<Ingredient, IngredientDetails>()
            .ForMember(d => d.IngredientCategory, opt => opt.MapFrom(s => s.IngredientCategory!.Name));
        config.CreateMap<IngredientForm, Ingredient>();
        config.CreateMap<Ingredient, IngredientForm>();
        config.CreateMap<User, UserDetail>();
        config.CreateMap<UserDetail, User>();
        config.CreateMap<Recipe, RecipeListItemDetail>();
        config.CreateMap<Recipe, RecipeDetail>();
        config.CreateMap<RecipeIngredientForm, RecipeIngredient>();
        config.CreateMap<RecipeIngredient, RecipeIngredientForm>();
        config.CreateMap<RecipeForm, Recipe>();
        config.CreateMap<Recipe, RecipeForm>();
    }
}
