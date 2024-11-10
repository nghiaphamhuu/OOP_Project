using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class GoodsReceiptService : IGoodsReceiptService
    {

        private IGoodsReceiptDAO _goodsReceiptDAO;

        private IStoreDAO _storeDAO;

        public GoodsReceiptService(IGoodsReceiptDAO goodsReceiptDAO, IStoreDAO storeDAO)
        {
            _goodsReceiptDAO = goodsReceiptDAO;
            _storeDAO = storeDAO;
        }
        public List<GoodsReceiptEntity> getAllGoodsReceipt()
        {
            return _goodsReceiptDAO.getAllGoodsReceipt();
        }

        public List<GoodsReceiptEntity> getGoodsReceiptBySearchName(string keySearch)
        {
            return _goodsReceiptDAO.getGoodsReceiptBySearchName(keySearch);
        }

        public void addGoodsReceipt(GoodsReceiptEntity goodsReceipt, bool isAddGoodsReceipt)
        {
            List<StoreEntity> stores = goodsReceipt.stores;
            Dictionary<int, StoreEntity> map = new Dictionary<int, StoreEntity>();

            foreach(StoreEntity store in stores)
            {
                if (map.ContainsKey(store.productId))
                {
                    StoreEntity itemUpdate = store;
                    int quantity = map[store.productId].quantity;
                    int total = map[store.productId].total;

                    itemUpdate.quantity = quantity + itemUpdate.quantity;
                    itemUpdate.total = total+ itemUpdate.total;

                    map[store.productId] =  itemUpdate;
                }
                else
                {
                    map.Add(store.productId, store);
                }
            }

            List<StoreEntity> mergeMap = new List<StoreEntity>();

            foreach(int k in map.Keys)
            {
                mergeMap.Add(map[k]);
            }

            if (isAddGoodsReceipt)
            {
                if (string.IsNullOrEmpty(goodsReceipt.goodsReceiptCode))
                {
                    throw new Exception("Please Input Goods Receipt Code!");
                }

                List<GoodsReceiptEntity> listCheck = getAllGoodsReceipt();

                foreach (GoodsReceiptEntity godReceipt in listCheck)
                {
                    if (godReceipt.goodsReceiptCode.Equals(goodsReceipt.goodsReceiptCode))
                    {
                        throw new Exception("This Goods Receipt Code is already Exist, Please Input another Goods Receipt Code!");
                    }
                }
                _goodsReceiptDAO.addGoodsReceipt(goodsReceipt);
            }

            _storeDAO.addStoreByGoodsReceipt(mergeMap);
        }

        public GoodsReceiptEntity getGoodsReceiptDetail(string goodsReceiptCode)
        {
            return _goodsReceiptDAO.getGoodsReceiptDetail(goodsReceiptCode);
        }

        public void updateGoodsReceipt(GoodsReceiptEntity goodsReceipt)
        {
            deleteGoodsReceipt(goodsReceipt.goodsReceiptCode, false);
            addGoodsReceipt(goodsReceipt, false);

            _goodsReceiptDAO.updateGoodsReceipt(goodsReceipt);
        }

        public void deleteGoodsReceipt(string goodsReceiptCode, bool isDeleteGoodsReceipt)
        {
            List<StoreEntity> stores = getGoodsReceiptDetail(goodsReceiptCode).stores;
            Dictionary<int, StoreEntity> map = new Dictionary<int, StoreEntity>();

            foreach (StoreEntity store in stores)
            {
                if (map.ContainsKey(store.productId))
                {
                    StoreEntity itemUpdate = store;
                    int quantity = map[store.productId].quantity;
                    int total = map[store.productId].total;

                    itemUpdate.quantity = quantity + itemUpdate.quantity;
                    itemUpdate.total = total + itemUpdate.total;

                    map[store.productId] = itemUpdate;
                }
                else
                {
                    map.Add(store.productId, store);
                }
            }

            List<StoreEntity> mergeMap = new List<StoreEntity>();

            foreach (int k in map.Keys)
            {
                mergeMap.Add(map[k]);
            }
            
            if(isDeleteGoodsReceipt)
            {
                _goodsReceiptDAO.deleteGoodsReceipt(goodsReceiptCode);
            }

            _storeDAO.deleteStoreByGoodsReceipt(mergeMap);
        }
    }
}
