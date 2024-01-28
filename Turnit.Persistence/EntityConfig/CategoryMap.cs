using FluentNHibernate.Mapping;
using TurnitStore.Domain.Entities;

namespace Turnit.Persistence.EntityConfig;

public class CategoryMap : ClassMap<Category>
{
    public CategoryMap()
    {
        Schema("public");
        Table("category");

        Id(x => x.Id, "id");
        Map(x => x.Name, "name");


        HasManyToMany(x => x.Products)
           .Cascade.All()
           .Inverse();
    }
}