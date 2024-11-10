using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class CategoryService : ICategoryService
    {

		private ICategoryDAO _categoryDAO;

		public CategoryService(ICategoryDAO categoryDAO)
		{
			_categoryDAO = categoryDAO;
		}
		public List<CategoryEntity> getAllCategory()
        {
            return _categoryDAO.getAllCategory();
        }

        public List<CategoryEntity> getCategoryBySearchName(string keySearch)
        {
            return _categoryDAO.getCategoryBySearchName(keySearch);
        }

        public void addCategory(CategoryEntity category)
        {
            List<CategoryEntity> listCheck = getAllCategory();
            foreach (CategoryEntity cate in listCheck)
            {
                if (cate.typeCd.Equals(category.typeCd))
                {
                    throw new Exception("This Type Code is already Exist, Please Input another Type Code!");
                }
            }
            _categoryDAO.addCategory(category);
        }

        public CategoryEntity getCategoryDetail(string typeCd)
        {
            return _categoryDAO.getCategoryDetail(typeCd);
        }

        public void updateCategory(CategoryEntity category)
        {
			_categoryDAO.updateCategory(category);
        }

        public void deleteCategory(string typeCd)
        {
			_categoryDAO.deleteCategory(typeCd);
        }
    }
}
