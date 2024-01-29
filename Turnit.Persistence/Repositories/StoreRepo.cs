using NHibernate;
using NHibernate.Linq;
using Turnit.GenericStore.Api.Entities;
using TurnitStore.Domain.Entities;
using TurnitStore.Domain.RepositoryInterfaces;
using static System.Formats.Asn1.AsnWriter;

namespace Turnit.Persistence.Repositories
{
    public class StoreRepo(ISession session) : IStoreRepo
    {
        public async Task<Store> GetStore(Guid storeId)
        {
            var store = await session.QueryOver<Store>()
                .Where(store => store.Id == storeId)
                .Left.JoinQueryOver(store => store.ProductAvailability)
                .Left.JoinQueryOver(avaliablity => avaliablity.Product)
                .SingleOrDefaultAsync();

            return store;
        }

        public async Task UpdateStore(Store store)
        {
            using var transaction = session.BeginTransaction();
            await session.SaveOrUpdateAsync(store);
            transaction.Commit();
        }
    }
}
