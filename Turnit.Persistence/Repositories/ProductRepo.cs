using NHibernate;
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;

namespace Turnit.Persistence.Repositories
{
    public class ProductRepo(ISession session) : IProductRepo
    {
        public async Task<Category> GetCategory(Guid categoryId)
        {
            var category = await session.QueryOver<Category>()
              .Where(p => p.Id == categoryId)
              .SingleOrDefaultAsync<Category>();

            return category;
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            var product = await session.QueryOver<Product>()
                .Where(p => p.Id == productId)
                .Left
                .JoinQueryOver<ProductAvailability>(p => p.Availability)
                .Left
                .JoinQueryOver<Category>(p => p.Product.Categories)
                .SingleOrDefaultAsync<Product>();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            using (var transaction = session.BeginTransaction())
            {
                await session.SaveOrUpdateAsync(product);
                transaction.Commit();
            }
            return product;

        }
    }
}
