using System;

namespace Turnit.GenericStore.Api.Features.Sales.Models
{
    public class ProductBookingDto
    {
        public int Quantity { get; set; }

        public Guid StoreId { get; set; }
    }
}
