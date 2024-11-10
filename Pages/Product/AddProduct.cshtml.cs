using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Service;
using WebApplication2.Entity;

namespace WebApplication2.Pages.Product
{
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public int id { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string date { get; set; }
        [BindProperty]
        public string company { get; set; }
        [BindProperty]
        public string dateOfProduce { get; set; }
        [BindProperty]
        public string type { get; set; }
        [BindProperty]
        public string price { get; set; }
        [BindProperty]
        public string result { get; set; }

        private IProductService _productService;

        public AddProductModel():base()
        {
            _productService = ObjectCreator.createProductService();
        }
		public void OnGet()
        {
            result = "Please add information of product!";
        }

        public IActionResult OnPost() 
        {
            try
            {
                ProductEntity product = new ProductEntity(name, date, company, dateOfProduce, type, price);
				_productService.addProduct(product);
                result = "Add Success";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return Page();
            }
        }
    }
}
