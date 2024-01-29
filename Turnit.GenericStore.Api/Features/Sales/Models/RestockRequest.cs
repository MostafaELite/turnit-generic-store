using System;

namespace Turnit.GenericStore.Api.Features.Sales.Models
{
    public class RestockRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
