using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public interface IProductService
	{
		List<ProductEntity> getAllProduct();

		List<ProductEntity> getProductBySearchName(string keySearch);

		void addProduct(ProductEntity product);

		ProductEntity getProductDetail(int id);

		void updateProduct(ProductEntity product);

		void deleteProduct(int id);

		List<ProductEntity> getProductsByCategory(string category);

		List<ProductEntity> getProductOutOfDate();
	}
}
