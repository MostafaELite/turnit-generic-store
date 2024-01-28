using NHibernate;
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;

namespace Turnit.Persistence.Repositories
{
    public class CategoryRepo(ISession session) : ICategoryRepo
    {
        public async Task<IEnumerable<Category>> GetCategories(bool includeProducts = false)
        {
            IQueryOver<Category> query = includeProducts ?
                session.QueryOver<Category>() :
                ProductCategoryQuery;

            return await query.ListAsync();
        }

        public async Task<Category> GetCategory(Guid categoryId)
        {
            //check where it's filtering
            var category = await ProductCategoryQuery
                .Where(c => c.Id == categoryId)
                .SingleOrDefaultAsync();

            return category;
        }

        private IQueryOver<Category, ProductAvailability> ProductCategoryQuery =>
            session.QueryOver<Category>()
            .Left
            .JoinQueryOver<Product>(category => category.Products)
            .JoinQueryOver<ProductAvailability>(product => product.Availability);
    }
}
