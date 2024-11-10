using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public class InvoiceService :IInvoiceService
	{
		private IInvoiceDAO _invoiceDAO;

        private IStoreDAO _storeDAO;

        private StoreService _storeService = new StoreService(new StoreDAO());
        public InvoiceService(IInvoiceDAO invoiceDAO, IStoreDAO storeDAO)
        {
            _invoiceDAO = invoiceDAO;
            _storeDAO = storeDAO;
        }
        public List<InvoiceEntity> getAllInvoice()
        {
            return _invoiceDAO.getAllInvoice();
        }

        public List<InvoiceEntity> getInvoiceBySearchName(string keySearch)
        {
            return _invoiceDAO.getInvoiceBySearchName(keySearch);
        }

        public void addInvoice(InvoiceEntity invoice, bool isAddInvoie)
        {
			List<StoreEntity> stores = invoice.stores;
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

            _storeDAO.addStoreByInvoice(mergeMap);

            if (isAddInvoie)
            {
				_invoiceDAO.addInvoice(invoice);
            }
        }

        public InvoiceEntity getInvoiceDetail(string invoiceCode)
        {
            return _invoiceDAO.getInvoiceDetail(invoiceCode);
        }

        public void updateInvoice(InvoiceEntity invoice)
        {
            deleteInvoice(invoice.invoiceCode, false);
            addInvoice(invoice, false);
			_invoiceDAO.updateInvoice(invoice);
        }

        public void deleteInvoice(string invoiceCode, bool isDeleteInvoice)
        {
            List<StoreEntity> stores = getInvoiceDetail(invoiceCode).stores;
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

            _storeDAO.deleteStoreByInvoice(mergeMap);

            if (isDeleteInvoice)
            {
				_invoiceDAO.deleteInvoice(invoiceCode);
            }
        }

        public int checkValid(InvoiceEntity invoice)
        {
            List<StoreEntity> stores = _storeService.getAllStore();
            List<StoreEntity> newList = invoice.stores;
            List<StoreEntity> oldList = getInvoiceDetail(invoice.invoiceCode).stores;
            Dictionary<int, StoreEntity> map1 = new Dictionary<int, StoreEntity>();
            Dictionary<int, StoreEntity> map2 = new Dictionary<int, StoreEntity>();
            Dictionary<int, StoreEntity> map3 = new Dictionary<int, StoreEntity>();

            foreach (StoreEntity store in newList)
            {
                if(map1.ContainsKey(store.productId))
                {
                    StoreEntity item = store;
                    item.quantity = item.quantity + map1[store.productId].quantity;

                    map1[store.productId] = item;
                }
                else
                {
                    map1.Add(store.productId, store);
                }
            }

            foreach (StoreEntity store in oldList)
            {
                if (map1.ContainsKey(store.productId))
                {
                    StoreEntity item = store;
                    item.quantity = item.quantity + map2[store.productId].quantity;

                    map2[store.productId] = item;
                }
                else
                {
                    map2.Add(store.productId, store);
                }
            }

            foreach (StoreEntity store in stores)
            {
                map3.Add(store.productId, store);
            }

            for(int i = 0; i< newList.Count; i++)
            {
                StoreEntity store = newList[i];
                if (map1[store.productId].quantity - map2[store.productId].quantity > map3[store.productId].quantity)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
