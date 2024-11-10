using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Statistics
{
    public class StoreByCategoryModel : PageModel
    {
        [BindProperty]
        public string selectedCategory { get; set; }
        [BindProperty]
        public List<CategoryEntity> categorys { get; set; }
        [BindProperty]
        public List<StoreEntity> stores { get; set; }
        private ICategoryService _categoryService;
        private IStoreService _storeService;

        public StoreByCategoryModel() : base()
        {
            _categoryService = ObjectCreator.createCategoryService();
            _storeService = ObjectCreator.createStoreService();
        }
        public void OnGet()
        {
            categorys = _categoryService.getAllCategory();
            stores = _storeService.getAllStore();
        }

        public void OnPost() 
        {
            string category = selectedCategory;
            stores = _storeService.getStoresByCategory(category);
        }
        
    }
}
