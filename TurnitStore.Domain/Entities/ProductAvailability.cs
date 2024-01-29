using System;
using TurnitStore.Domain.Entities;

namespace Turnit.GenericStore.Api.Entities;

public class ProductAvailability
{
    //Parameterless constructor and access modifiers are needed by NHibernate, should add a sepreate persistence model and keep the domain clean, but ain't nobody got time for that
    public ProductAvailability() { }

    public ProductAvailability(Store store, Product product, int quantity)
    {
        Store = store;
        Product = product;
        Availability = quantity;
    }

    public virtual Guid Id { get; set; }

    public virtual Product Product { get; set; }

    public virtual Store Store { get; set; }

    public virtual int Availability { get; set; }

    //should be internal
    protected internal virtual int BookProduct(int desiredQuantity)
    {
        if (desiredQuantity > Availability)
            throw new Exception($"Can't book {desiredQuantity} of product {Id}, only {Availability} item was avaliable");

        Availability -= desiredQuantity;
        return Availability;
    }

    //should be internal
    protected internal virtual int StockProduct(int quantity)
    {
        if (quantity < 1)
            throw new Exception($"Bruv, what are u doing?");

        Availability += quantity;
        return Availability;
    }
}
