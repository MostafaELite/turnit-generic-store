using System;
using TurnitStore.Domain.Entities;

namespace Turnit.GenericStore.Api.Entities;

public class ProductCategory
{
    public virtual Guid Id { get; set; }

    public virtual required Product Product { get; set; }

    public virtual required Category Category { get; set; }
}
