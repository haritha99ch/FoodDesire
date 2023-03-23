namespace FoodDesire.IMS.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<Ingredient, IngredientDetails>()
            .ForMember(d => d.IngredientCategory, opt => opt.MapFrom(s => s.IngredientCategory!.Name));
        config.CreateMap<IngredientForm, Ingredient>();
        config.CreateMap<Ingredient, IngredientForm>();
        config.CreateMap<User, UserDetail>();
        config.CreateMap<UserDetail, User>();
    }
}
