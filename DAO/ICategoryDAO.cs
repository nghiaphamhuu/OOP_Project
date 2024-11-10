using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public interface ICategoryDAO
	{
		List<CategoryEntity> getAllCategory();

		void addCategory(CategoryEntity category);

		CategoryEntity getCategoryDetail(string typeCd);

		List<CategoryEntity> getCategoryBySearchName(string keySearch);

		void updateCategory(CategoryEntity category);

		void deleteCategory(string typeCd);
	}
}
