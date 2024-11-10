using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public interface IProductDAO
	{
		List<ProductEntity> getAllProduct();

		void addProduct(ProductEntity product);

		ProductEntity getProductDetail(int id);

		List<ProductEntity> getProductBySearchName(string keySearch);

		void updateProduct(ProductEntity product);

		void deleteProduct(int id);

		List<ProductEntity> getProductsByCategory(string category);
	}
}
