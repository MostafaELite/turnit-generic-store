using System;
using TurnitStore.Domain.Entities;

namespace Turnit.GenericStore.Api.Entities;

public class ProductAvailability
{
    public virtual Guid Id { get; set; }

    public virtual required Product Product { get; set; }

    public virtual required Store Store { get; set; }

    public virtual int Availability { get; set; }

    public virtual int BookProduct(int desiredQuantity)
    {
        if (desiredQuantity > Availability)
            throw new Exception($"Can't book {desiredQuantity} of product {Id}, only {Availability} item was avaliable");

        Availability -= desiredQuantity;
        return Availability;
    }
}
