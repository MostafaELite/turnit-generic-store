using System;
using System.Threading.Tasks;
using Turnit.GenericStore.Api.Features.Sales.Models;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.Models;

namespace TurnitStore.Domain.Services
{
    public interface IProductService
    {
        Task<Product> GetById(Guid productId);

        Task<ResultWrapper<Product>> AssociateToCategory(Guid productId, Guid categoryId);

        Task<ResultWrapper<Product>> DisassociateFromCategory(Guid productId, Guid categoryId);

        Task<ResultWrapper<Product>> Book(Guid productId, IEnumerable<ProductBookingDto> bookings);

        Task<IEnumerable<Product>> GetUncategorizedProducts();
    }
}