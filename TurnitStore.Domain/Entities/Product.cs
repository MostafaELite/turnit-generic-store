using System;
using Turnit.GenericStore.Api.Entities;

namespace TurnitStore.Domain.Entities;

public class Product
{
    public virtual Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual required string Description { get; set; }

    public virtual required ICollection<Category> Categories { get; set; }

    public virtual required ICollection<ProductAvailability> Availability { get; set; }
}
