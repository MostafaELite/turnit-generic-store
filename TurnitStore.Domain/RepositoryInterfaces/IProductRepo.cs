using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;

namespace TurnitStore.Domain.RepositoryInterfaces
{
    public interface IProductRepo
    {
        Task<Category> GetCategory(Guid categoryId);

        Task<Product> GetProduct(Guid productId);

        Task<Product> Update(Product product);
    }

    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetCategories(bool includeProducts = false);

        Task<Category> GetCategory(Guid categoryId);
    }
    public interface IStoreRepo
    {
        Task<Store> GetStore(Guid storeId);

        Task UpdateStore(Store store);
    }
}
