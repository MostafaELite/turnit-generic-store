using System;
using System.Threading.Tasks;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.Models;

namespace TurnitStore.Domain.Services
{
    public interface IProductService
    {
        Task<ResultWrapper<Product>> AssociateToCategory(Guid productId, Guid categoryId);

        Task<ResultWrapper<Product>> DisassociateFromCategory(Guid productId, Guid categoryId);

        Task<ResultWrapper<IEnumerable<OrderDetails>>> Book(Guid productId, int desiredQuantity, Guid? perferedStoreId);

        Task<Product> GetUncategorizedProducts();
    }
}