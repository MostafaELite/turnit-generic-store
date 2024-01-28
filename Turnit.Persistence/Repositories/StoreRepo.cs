using NHibernate;
using NHibernate.Linq;
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;

namespace Turnit.Persistence.Repositories
{
    public class StoreRepo(ISession session) : IStoreRepo
    {
        public Task<Store> GetStore(Guid storeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStore(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
