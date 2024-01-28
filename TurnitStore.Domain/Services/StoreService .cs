
using TurnitStore.Domain.Models;
using TurnitStore.Domain.RepositoryInterfaces;

namespace TurnitStore.Domain.Services
{
    public class StoreService(IStoreRepo repo) : IStoreService
    {
        public async Task<ResultWrapper<OrderDetails>> RestockAsync(Guid storeId, int quantity)
        {
            var store = await repo.GetStore(storeId);
            if (store is null)
                return ResultWrapper<OrderDetails>.Faliure(ResultStatus.NotFound, $"The store {storeId} is not found");


            foreach (var product in store.ProductAvailability)
                product.Availability += quantity;

            await repo.UpdateStore(store);

            return ResultWrapper<OrderDetails>.Success(null);
        }
    }
}