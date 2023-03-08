using AutoMapper;
using FoodDesire.Models;

namespace FoodDesire.IMS.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<Ingredient, IngredientDetail>()
            .ForMember(d => d.IngredientCategory, opt => opt.MapFrom(s => s.IngredientCategory!.Name));
    }
}
