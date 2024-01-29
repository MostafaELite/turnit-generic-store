using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Models;

namespace TurnitStore.Domain.Services
{
    public interface IStoreService
    {
        Task<ResultWrapper<ProductAvailability>> RestockAsync(Guid storeId, Guid productId, int quantity);
    }
}