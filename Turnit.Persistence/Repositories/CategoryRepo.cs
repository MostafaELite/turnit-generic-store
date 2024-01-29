using NHibernate;
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;

namespace Turnit.Persistence.Repositories
{
    public class CategoryRepo(ISession session) : ICategoryRepo
    {
        //The includes can be passed from the business layer in a db agnostic way and/or set on a repo level to avoid passing the params for every method
        public async Task<IEnumerable<Category>> GetCategories(bool includeProducts = false)
        {
            var query = session.QueryOver<Category>();
            return includeProducts ?
                        await GetProductQuery(query).ListAsync() :
                        await query.ListAsync();
        }

        public async Task<Category> GetCategory(Guid categoryId, bool includeProducts = false)
        {
            var filteredQuery = session
                .QueryOver<Category>()
                .Where(cat => cat.Id == categoryId);

            return includeProducts ?
                await GetProductQuery(filteredQuery).SingleOrDefaultAsync() :
                await filteredQuery.SingleOrDefaultAsync();
        }

        //check where it's filtering/deffered exec like IQueryable
        private IQueryOver<Category, ProductAvailability> GetProductQuery(IQueryOver<Category, Category> query) => query
                .Left
                .JoinQueryOver<Product>(category => category.Products)
                .JoinQueryOver<ProductAvailability>(product => product.Availability);
    }
}
