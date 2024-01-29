using TurnitStore.Domain.Entities;

namespace TurnitStore.Domain.RepositoryInterfaces
{
    public interface IProductRepo
    {
        Task<Product> GetProduct(Guid productId);

        Task<IEnumerable<Product>> GetUncategorizedProducts();

        Task<Product> Update(Product product);
    }
}
