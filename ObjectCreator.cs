using WebApplication2.DAO;
using WebApplication2.Service;

namespace WebApplication2
{
	public class ObjectCreator
	{
		public static IProductService createProductService()
		{
			return new ProductService(new ProductDAO());
		}

		public static ICategoryService createCategoryService()
		{
			return new CategoryService(new CategoryDAO());
		}

        public static IGoodsReceiptService createGoodsReceiptService()
        {
            return new GoodsReceiptService(new GoodsReceiptDAO(), new StoreDAO());
        }

		public static IInvoiceService createInvoiceService()
		{
			return new InvoiceService(new InvoiceDAO(), new StoreDAO());
		}

        public static IStoreService createStoreService()
        {
            return new StoreService(new StoreDAO());
        }
    }
}
