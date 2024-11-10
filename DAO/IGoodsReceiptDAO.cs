using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public interface IGoodsReceiptDAO
    {
        List<GoodsReceiptEntity> getAllGoodsReceipt();

        void addGoodsReceipt(GoodsReceiptEntity goodsReceipt);

        GoodsReceiptEntity getGoodsReceiptDetail(string goodsReceiptCode);

        List<GoodsReceiptEntity> getGoodsReceiptBySearchName(string keySearch);

        void updateGoodsReceipt(GoodsReceiptEntity goodsReceipt);

        void deleteGoodsReceipt(string goodsReceiptCode);
    }
}
