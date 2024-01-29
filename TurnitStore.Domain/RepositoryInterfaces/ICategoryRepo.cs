using TurnitStore.Domain.Entities;

namespace TurnitStore.Domain.RepositoryInterfaces
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetCategories(bool includeProducts = false);

        Task<Category> GetCategory(Guid categoryId, bool includeProducts);
    }
}
