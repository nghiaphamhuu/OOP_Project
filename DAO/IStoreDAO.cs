using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public interface IStoreDAO
    {
        List<StoreEntity> getAllStore();
        void addStoreByGoodsReceipt(List<StoreEntity> stores);
        void deleteStoreByGoodsReceipt(List<StoreEntity> mergeMap);
        StoreEntity getStoreDetail(int productId);
        void addStoreByInvoice(List<StoreEntity> stores);
        void deleteStoreByInvoice(List<StoreEntity> mergeMap);
    }
}
