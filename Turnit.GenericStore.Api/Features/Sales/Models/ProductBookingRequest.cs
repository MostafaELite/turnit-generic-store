using System;

namespace Turnit.GenericStore.Api.Features.Sales.Models
{
    public class ProductBookingRequest
    {
        public int Quantity { get; set; }

        public Guid? PerferedStoreId { get; set; }
    }
}
