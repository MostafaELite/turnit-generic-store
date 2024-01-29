
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Enums;
using TurnitStore.Domain.Models;
using TurnitStore.Domain.RepositoryInterfaces;

namespace TurnitStore.Domain.Services
{
    public class StoreService(IStoreRepo repo, IProductService products) : IStoreService
    {
        public async Task<ResultWrapper<ProductAvailability>> RestockAsync(Guid storeId, Guid productId, int quantity)
        {
            var store = await repo.GetStore(storeId);
            if (store is null)
                return ResultWrapper<ProductAvailability>.Faliure(ResultStatus.NotFound, $"The store {storeId} is not found");


            var productAvailablity = await EnsureProductInStore(store, productId);
            productAvailablity.StockProduct(quantity);

            await repo.UpdateStore(store);

            return ResultWrapper<ProductAvailability>.Success(productAvailablity);
        }

        private async Task<ProductAvailability> EnsureProductInStore(Store store, Guid productId)
        {
            var productAvailablity = store.ProductAvailability.FirstOrDefault(availablity => availablity.Product.Id == productId);

            if (productAvailablity is null)
            {
                //validate if such a product with this id even exists
                var product = await products.GetById(productId);

                productAvailablity = new ProductAvailability(store, product, 0);
                store.ProductAvailability = store.ProductAvailability.Append(productAvailablity);
            }
            
            return productAvailablity;
        }
    }
}