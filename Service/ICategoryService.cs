using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public interface ICategoryService
	{
		List<CategoryEntity> getAllCategory();

		List<CategoryEntity> getCategoryBySearchName(string keySearch);

		void addCategory(CategoryEntity category);

		CategoryEntity getCategoryDetail(string typeCd);

		void updateCategory(CategoryEntity category);

		void deleteCategory(string typeCd);
	}
}
