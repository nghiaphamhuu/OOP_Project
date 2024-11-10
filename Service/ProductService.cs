using Microsoft.AspNetCore.Http;
using System.Globalization;
using WebApplication2.DAO;
using WebApplication2.Entity;
namespace WebApplication2.Service

{
    public class ProductService : IProductService
    {
        private IProductDAO _productDAO;

        public ProductService(IProductDAO productDAO)
		{
			_productDAO = productDAO;
		}

		 public List<ProductEntity> getAllProduct()
        {
            return _productDAO.getAllProduct();
        }

        public List<ProductEntity> getProductBySearchName(string keySearch)
        {
            return _productDAO.getProductBySearchName(keySearch);
        }

        public void addProduct(ProductEntity product)
        {
            List<ProductEntity> products = getAllProduct();
            
            foreach( ProductEntity pd in  products)
            {
                if(pd.name == product.name)
                {
                    throw new Exception("This name of product is already have!");
                }
            }
            _productDAO.addProduct(product);
            return ;
        }

        public ProductEntity getProductDetail(int id)
        {
            return _productDAO.getProductDetail(id);
        }

        public void updateProduct(ProductEntity product)
        {
			_productDAO.updateProduct(product);
        }

        public void deleteProduct(int id)
        {
			_productDAO.deleteProduct(id);
        }

        public List<ProductEntity> getProductsByCategory(string category)
        {
            if(string.IsNullOrEmpty(category))
            {
                return getAllProduct();
            }

            return _productDAO.getProductsByCategory(category);
        }

        public List<ProductEntity> getProductOutOfDate()
        {
            List<ProductEntity> products = getAllProduct();
            List<ProductEntity> result = new List<ProductEntity>();
            DateTime now = DateTime.Now;

            foreach(ProductEntity product in products)
            {
                
                if (DateTime.TryParseExact(product.date, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime err))
                {
                    DateTime date = DateTime.ParseExact(product.date, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    if (now > date)
                    {
                        result.Add(product);
                    }
                }
            }

            return result;
        }
    }
}
