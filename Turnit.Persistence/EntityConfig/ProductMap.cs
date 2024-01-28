using FluentNHibernate.Mapping;
using TurnitStore.Domain.Entities;

namespace Turnit.GenericStore.Api.Entities;

public class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Schema("public");
        Table("product");

        Id(x => x.Id, "id");
        Map(x => x.Name, "name");
        Map(x => x.Description, "description");

        HasManyToMany(x => x.Categories)
            .ParentKeyColumn("product_id")
            .ChildKeyColumn("category_id")
            .Cascade
            .SaveUpdate()
            .Table("product_category");

        HasMany(product => product.Availability);
    }
}