using TurnitStore.Domain.Models;

namespace TurnitStore.Domain.Services
{
    public interface IStoreService
    {
        Task<ResultWrapper<OrderDetails>> RestockAsync(Guid storeId, int quantity);
    }
}