using FluentNHibernate.Mapping;

namespace Turnit.GenericStore.Api.Entities;

public class ProductCategoryMap : ClassMap<ProductCategory>
{
    public ProductCategoryMap()
    {
        Schema("public");
        Table("product_category");

        Id(x => x.Id, "id").GeneratedBy.Guid().UnsavedValue(Guid.NewGuid());
        References(x => x.Category, "category_id");
        References(x => x.Product, "product_id");
    }
}