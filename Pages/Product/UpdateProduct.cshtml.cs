using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Product
{
    public class UpdateProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
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

        [BindProperty]
        public ProductEntity product { get; set; }

        private IProductService _productService;

        public UpdateProductModel() : base()
        {
            _productService = ObjectCreator.createProductService();
        }
        public void OnGet()
        {
            product = _productService.getProductDetail(id);
            if (product == null)
            {
                result = "Id is not valid";
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                ProductEntity product = new ProductEntity(name, date, company, dateOfProduce, type, price);
                _productService.updateProduct(product);
			}
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return RedirectToPage("./Index");
		}
    }
}
