using Newtonsoft.Json;
using WebApplication2.Entity;
using static System.Formats.Asn1.AsnWriter;

namespace WebApplication2.DAO
{
	public class StoreDAO : IStoreDAO
	{
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Store.txt";
        public List<StoreEntity> getAllStore()
        {
            List<StoreEntity> stores = new List<StoreEntity>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                StoreEntity store = JsonConvert.DeserializeObject<StoreEntity>(line);
                stores.Add(store);
            }

            reader.Close();

            return stores;
        }

        public void addStoreByGoodsReceipt(List<StoreEntity> stores)
        {
            List<StoreEntity> list = getAllStore();
            List<StoreEntity> addList = new List<StoreEntity>();

            //Update data in store with productId Has already in store
            foreach(StoreEntity store in list)
            {
                StoreEntity item = store;
                for ( int i = 0; i< stores.Count; i++)
                {
                    StoreEntity item2  = stores[i];
                    if (item2.productId == item.productId)
                    {
                        item.quantity = item2.quantity + item.quantity;
                        item.total = item2.total +item.total;
                        break;
                    }
                }
                addList.Add(item);
            }

            //Add data to store with new product id
            foreach(StoreEntity item in stores)
            {
                bool chk = true;
                foreach(StoreEntity item2 in list)
                {
                    if(item2.productId == item.productId)
                    {
                        chk = false;
                        break;
                    }
                }

                if(chk)
                {
                    addList.Add(item);
                }

            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (StoreEntity addItem in addList)
            {
                string json = JsonConvert.SerializeObject(addItem);

                writer.WriteLine(json);
            }

       
            writer.Close();
        }

        public void deleteStoreByGoodsReceipt(List<StoreEntity> mergeMap)
        {
            List<StoreEntity> list = getAllStore();

            List<StoreEntity> deleteList = new List<StoreEntity>();

            foreach(StoreEntity item in list)
            {
                StoreEntity itemDelete = item;
                foreach(StoreEntity item2 in mergeMap)
                {
                    if(item2.productId == item.productId)
                    {
                        itemDelete.quantity = itemDelete.quantity - item2.quantity;
                        itemDelete.total = itemDelete.total - item2.total;
                        break;
                    }
                }

                if(itemDelete.quantity > 0)
                {
                    deleteList.Add(itemDelete);
                }
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (StoreEntity deleteItem in deleteList)
            {
                string json = JsonConvert.SerializeObject(deleteItem);

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public StoreEntity getStoreDetail(int productId)
        {
            StoreEntity result = new StoreEntity();

            List<StoreEntity> list = getAllStore();

            foreach(StoreEntity item in list)
            {
                if(productId == item.productId)
                {
                    return item;
                }
            }

            return result;
        }

		public void addStoreByInvoice(List<StoreEntity> stores)
		{
            List<StoreEntity> list = getAllStore();
            List<StoreEntity> addList = new List<StoreEntity>();

            //Update data in store with productId Has already in store
            foreach (StoreEntity store in list)
            {
                StoreEntity item = store;
                for (int i = 0; i < stores.Count; i++)
                {
                    StoreEntity item2 = stores[i];
                    if (item2.productId == item.productId)
                    {
                        item.quantity =  item.quantity - item2.quantity ;
                        item.total = item.total - item2.total;
                        break;
                    }
                }

                if(item.quantity > 0)
                {
                    addList.Add(item);
                }
                
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (StoreEntity addItem in addList)
            {
                string json = JsonConvert.SerializeObject(addItem);

                writer.WriteLine(json);
            }


            writer.Close();
        }

		public void deleteStoreByInvoice(List<StoreEntity> mergeMap)
		{
            List<StoreEntity> list = getAllStore();

            List<StoreEntity> deleteList = new List<StoreEntity>();

            foreach (StoreEntity item in list)
            {
                StoreEntity itemDelete = item;
                foreach (StoreEntity item2 in mergeMap)
                {
                    if (item2.productId == item.productId)
                    {
                        itemDelete.quantity = itemDelete.quantity + item2.quantity;
                        itemDelete.total = itemDelete.total + item2.total;
                        break;
                    }
                }

                if (itemDelete.quantity > 0)
                {
                    deleteList.Add(itemDelete);
                }
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (StoreEntity deleteItem in deleteList)
            {
                string json = JsonConvert.SerializeObject(deleteItem);

                writer.WriteLine(json);
            }

            writer.Close();
        }
	}
}
