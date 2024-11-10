using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public class StoreService : IStoreService
	{
        private IStoreDAO _storeDAO;

        public StoreService(IStoreDAO storeDAO)
        {
            _storeDAO = storeDAO;
        }
        public StoreEntity getStoreDetail(int productId)
		{
			return _storeDAO.getStoreDetail(productId);
		}

		public List<StoreEntity> getAllStore()
		{
			return _storeDAO.getAllStore();
		}

		public List<StoreEntity> getStoresByCategory(string category)
		{
			List<StoreEntity> result = new List<StoreEntity>();
			List<StoreEntity> list = getAllStore();

			foreach (StoreEntity store in list)
			{
				StoreEntity item = store;

				if(item.typeCd.Equals(category))
				{
					result.Add(item);
				}
			}

			return result;
		}

    }
}
