using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public interface IStoreService
    {
        StoreEntity getStoreDetail(int productId);
        List<StoreEntity> getAllStore();
        List<StoreEntity> getStoresByCategory(string category);
    }
}
