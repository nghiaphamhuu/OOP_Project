using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Statistics
{
    public class ProductOutOfDateModel : PageModel
    {
		[BindProperty]
		public List<ProductEntity> products { get; set; }
		private IProductService _productService;

		public ProductOutOfDateModel() : base()
		{
			_productService = ObjectCreator.createProductService();
		}
		public void OnGet()
        {
			products = _productService.getProductOutOfDate();
		}
    }
}
