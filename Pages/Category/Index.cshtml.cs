using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Category
{
    public class IndexModel : PageModel
    {
		[BindProperty]
		public List<CategoryEntity> categorys { get; set; }

		[BindProperty]
		public string keySearch { get; set; }
		private ICategoryService _categoryService;

		public IndexModel() : base()
		{
			_categoryService = ObjectCreator.createCategoryService();
		}
		public void OnGet()
        {
			categorys = _categoryService.getAllCategory();
		}

		public void OnPost()
		{
			categorys = _categoryService.getCategoryBySearchName(keySearch);
		}
    }
}
