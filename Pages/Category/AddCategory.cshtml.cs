using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Category
{
    public class AddCategoryModel : PageModel
    {
        [BindProperty]
        public string typeCd { get; set; }
        [BindProperty]
        public string typeDesc { get; set; }
        [BindProperty]
        public string result { get; set; }

        private ICategoryService _categoryService;

        public AddCategoryModel() : base()
        {
            _categoryService = ObjectCreator.createCategoryService();
        }
        public void OnGet()
        {
            result = "Please add information of Category";
        }

        public IActionResult OnPost() 
        {
            try
            {
                CategoryEntity category = new CategoryEntity(typeCd, typeDesc);
                _categoryService.addCategory(category);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
