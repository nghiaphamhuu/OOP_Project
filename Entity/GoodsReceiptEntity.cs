namespace WebApplication2.Entity
{
    public class GoodsReceiptEntity
    {
        public string goodsReceiptCode { get; set; }
        public string insertDate { get; set; } 
        public List<StoreEntity> stores { get; set; }

        public GoodsReceiptEntity(string goodsReceiptCode, List<StoreEntity> stores) 
        {
            this.goodsReceiptCode = goodsReceiptCode;
            this.insertDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.stores = stores;
        }

        public GoodsReceiptEntity() { }
    }
}
