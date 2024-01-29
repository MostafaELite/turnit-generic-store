using Mapster;
using Turnit.GenericStore.Api.Models;
using TurnitStore.Domain.Entities;

namespace Turnit.GenericStore.Api
{
    public static class MapperConfig
    {
        public static TypeAdapterConfig GetConfig =>
            new TypeAdapterConfig()
            .NewConfig<Category, ProductCategoryModel>()
            .Map(dest => dest.CategoryId, src => src.Id)
            .Config;

    }
}
