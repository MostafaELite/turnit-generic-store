using Turnit.GenericStore.Api.Entities;

namespace TurnitStore.Domain.RepositoryInterfaces
{
    public interface IStoreRepo
    {
        Task<Store> GetStore(Guid storeId);

        Task UpdateStore(Store store);
    }
}
