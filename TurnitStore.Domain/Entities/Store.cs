using System;

namespace Turnit.GenericStore.Api.Entities;

public class Store
{
    public virtual Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual required IEnumerable<ProductAvailability> ProductAvailability { get; set; }
}
