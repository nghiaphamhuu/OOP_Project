using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;

namespace WebApplication2.Pages.Invoice
{
    public class DeleteStoreDetailModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string row { get; set; }
		[BindProperty(SupportsGet = true)]
		public string invoiceCode { get; set; }
		public IActionResult OnGet()
        {
			List<StoreEntity> stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateInvoice");
			int rowTmp = int.Parse(row);
			stores.Remove(stores[rowTmp]);
			HttpContext.Session.SetObject<List<StoreEntity>>("storesUpdateInvoice", stores);
			return RedirectToPage("./UpdateInvoice?continueSession=true&invoiceCode=" + invoiceCode);
		}
    }
}
