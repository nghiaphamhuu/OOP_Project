using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Product
{
    public class DeleteProductModel : PageModel
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
		public ProductEntity product { get; set; }

		private IProductService _productService;

		public DeleteProductModel() : base()
		{
			_productService = ObjectCreator.createProductService();
		}

		public string result { get; set; }
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
			_productService.deleteProduct(id);
			return RedirectToPage("./Index");
		}
    }
}
