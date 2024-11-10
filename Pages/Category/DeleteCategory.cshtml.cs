using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Category
{
    public class DeleteCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string typeCd { get; set; }
        [BindProperty]
        public string typeDesc { get; set; }
        [BindProperty]
        public string result { get; set; }

        private ICategoryService _categoryService;

        public DeleteCategoryModel() : base()
        {
            _categoryService = ObjectCreator.createCategoryService();
        }

        [BindProperty]
        public CategoryEntity category { get; set; }
        public void OnGet()
        {
            category = _categoryService.getCategoryDetail(typeCd);
        }

        public IActionResult OnPost()
        {
            try
            {
                _categoryService.deleteCategory(typeCd);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
