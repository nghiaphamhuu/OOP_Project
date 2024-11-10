using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public interface IGoodsReceiptService
    {
        List<GoodsReceiptEntity> getAllGoodsReceipt();

        List<GoodsReceiptEntity> getGoodsReceiptBySearchName(string keySearch);

        void addGoodsReceipt(GoodsReceiptEntity goodsReceipt, bool isAddGoodsReceipt);

        GoodsReceiptEntity getGoodsReceiptDetail(string goodsReceiptCode);

        void updateGoodsReceipt(GoodsReceiptEntity goodsReceipt);

        void deleteGoodsReceipt(string goodsReceiptCode, bool isDeleteGoodsReceipt);
    }
}
