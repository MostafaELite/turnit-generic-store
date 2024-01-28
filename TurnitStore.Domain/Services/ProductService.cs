using TurnitStore.Domain.Entities;
using TurnitStore.Domain.Models;
using TurnitStore.Domain.RepositoryInterfaces;

namespace TurnitStore.Domain.Services
{
    public class ProductService(IProductRepo repo) : IProductService
    {
        public async Task<ResultWrapper<Product>> AssociateToCategory(Guid productId, Guid categoryId)
        {
            var product = await repo.GetProduct(productId);

            //TODO: FLUSH
            //These string can be read from a static file or resources
            if (product.Categories.Any(cat => cat.Id == categoryId))
                return ResultWrapper<Product>.Faliure(ResultStatus.ShouldNotComplete, $"The category with the id {categoryId} is already assoicated with the product id {productId}");

            var category = await repo.GetCategory(categoryId);
            if (category is null)
                return ResultWrapper<Product>.Faliure(ResultStatus.NotFound, $"Unable to find the category {categoryId}");

            product.Categories.Add(category);
            await repo.Update(product);

            return ResultWrapper<Product>.Success(product);

        }

        public async Task<ResultWrapper<Product>> DisassociateFromCategory(Guid productId, Guid categoryId)
        {
            var product = await repo.GetProduct(productId);
            var categoryToRemove = product.Categories.FirstOrDefault(category => category.Id == categoryId);

            if (categoryToRemove is null)
                return ResultWrapper<Product>.Faliure(ResultStatus.NotFound, $"The category with the id {categoryId} is not assoicated with the product id {productId}");

            product.Categories.Remove(categoryToRemove);
            await repo.Update(product);

            return ResultWrapper<Product>.Success(product);
        }

        public async Task<ResultWrapper<IEnumerable<OrderDetails>>> Book(Guid productId, int desiredQuantity, Guid? perferedStoreId)
        {
            var product = await repo.GetProduct(productId);

            if (perferedStoreId is not null)
            {
                var perferedStore = product.Availability.FirstOrDefault(a => a.Store.Id == perferedStoreId);
                desiredQuantity = perferedStore is null ? desiredQuantity : perferedStore.Availability -= desiredQuantity;
            }

            var orderDetails = new List<OrderDetails>();

            //Not mentioned in the req, but we may need to sort desc by availablity to keep the product distrubtion across different stores
            foreach (var storeAvailablity in product.Availability)
            {
                if (desiredQuantity == 0)
                    break;

                var storeBookableQuantity = Math.Min(desiredQuantity, storeAvailablity.Availability);

                storeAvailablity.BookProduct(storeBookableQuantity);
                desiredQuantity -= storeBookableQuantity;

                orderDetails.Add(new OrderDetails(storeAvailablity.Id, storeBookableQuantity));
            }

            if (desiredQuantity > 0)
                return ResultWrapper<IEnumerable<OrderDetails>>.Faliure(ResultStatus.Insufficient, "You are asking for tooo much mate :P :D");

            await repo.Update(product);
            return ResultWrapper<IEnumerable<OrderDetails>>.Success(orderDetails);
        }
    }
}