using FluentNHibernate.Mapping;

namespace Turnit.GenericStore.Api.Entities;

public class StoreMap : ClassMap<Store>
{
    public StoreMap()
    {
        Schema("public");
        Table("product");

        Id(x => x.Id, "id");
        Map(x => x.Name, "name");
    }
}