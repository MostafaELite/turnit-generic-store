using System;

namespace TurnitStore.Domain.Entities;

public class Category
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
