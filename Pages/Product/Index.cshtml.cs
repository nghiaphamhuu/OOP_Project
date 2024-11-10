using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Product
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<ProductEntity> products { get; set; }

        [BindProperty]
        public string keySearch { get; set; }

		private IProductService _productService;

		public IndexModel() : base()
		{
			_productService = ObjectCreator.createProductService();
		}

		public void OnGet()
        {
            products = _productService.getAllProduct();
		}

        public void OnPost()
        {
            products = _productService.getProductBySearchName(keySearch);
        }
    }
}
