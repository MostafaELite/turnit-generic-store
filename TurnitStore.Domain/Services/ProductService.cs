using Turnit.GenericStore.Api.Features.Sales.Models;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.Enums;
using TurnitStore.Domain.Models;
using TurnitStore.Domain.RepositoryInterfaces;

namespace TurnitStore.Domain.Services
{
    //Services shouldn't directly consume other services repo, but no time to add a category service
    public class ProductService(IProductRepo repo, ICategoryRepo categories) : IProductService
    {
        public async Task<ResultWrapper<Product>> AssociateToCategory(Guid productId, Guid categoryId)
        {
            var product = await repo.GetProduct(productId);

            //TODO: FLUSH
            //These string can be read from a static file or resources
            if (product.Categories.Any(cat => cat.Id == categoryId))
                return ResultWrapper<Product>.Faliure(
                    ResultStatus.ShouldNotComplete,
                    $"The category with the id {categoryId} is already assoicated with the product id {productId}");

            var category = await categories.GetCategory(categoryId, true);
            if (category is null)
                return ResultWrapper<Product>.Faliure(ResultStatus.NotFound, $"Unable to find the category {categoryId}");

            //Should be moved to product entity
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

            //Should be moved to product entity
            product.Categories.Remove(categoryToRemove);
            await repo.Update(product);

            return ResultWrapper<Product>.Success(product);
        }

        public async Task<ResultWrapper<Product>> Book(Guid productId, IEnumerable<ProductBookingDto> bookings)
        {
            var product = await repo.GetProduct(productId);

            foreach (var booking in bookings)
            {
                var availabilityInStore = product.Availability.FirstOrDefault(availability => availability.Store.Id == booking.StoreId);

                if (availabilityInStore is null || availabilityInStore.Availability < booking.Quantity)
                    return ResultWrapper<Product>.Faliure(
                        ResultStatus.Insufficient,
                        $"Unable to book {booking.Quantity} from store {booking.StoreId}, only {availabilityInStore?.Availability} items were left");

                availabilityInStore.BookProduct(booking.Quantity);
            }

            await repo.Update(product);
            return ResultWrapper<Product>.Success(product);
        }

        public async Task<IEnumerable<Product>> GetUncategorizedProducts() => await repo.GetUncategorizedProducts();

        
        public Task<Product> GetById(Guid productId) => throw new NotImplementedException();
    }
}