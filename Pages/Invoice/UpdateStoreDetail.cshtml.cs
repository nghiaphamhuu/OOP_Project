using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Invoice
{
    public class UpdateStoreDetailModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string row { get; set; }
		[BindProperty]
		public string quantity { get; set; }
		[BindProperty]
		public string result { get; set; }
		[BindProperty]
		public StoreEntity storeDetail { get; set; }
		[BindProperty]
		public List<StoreEntity> stores { get; set; }
		private IInvoiceService _invoiceService;
		public UpdateStoreDetailModel() : base()
		{
			_invoiceService = ObjectCreator.createInvoiceService();
		}
		public void OnGet()
        {
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateInvoice");
			if (!string.IsNullOrEmpty(row)
				&& int.TryParse(row, out int err))
			{
				int rowTmp = int.Parse(row);
				storeDetail = stores[rowTmp];
			}
			else
			{
				result = "Row is InValid!!";
			}
		}

		public IActionResult OnPost()
		{
			if (!int.TryParse(quantity, out int err1))
			{
				result = "Quantity is Invalid";
			}
			else
			{
				if (!string.IsNullOrEmpty(row)
				&& int.TryParse(row, out int err))
				{
					int rowUpdate = int.Parse(row);
					List<StoreEntity> newStoreUpdate = new List<StoreEntity>();

					for (int i = 0; i < stores.Count; i++)
					{
						if (i == rowUpdate)
						{
							StoreEntity update = stores[i];
							update.quantity = int.Parse(Request.Form["quantity"]);
							newStoreUpdate.Add(update);
							continue;
						}
						newStoreUpdate.Add(stores[i]);
					}

					string invoiceCode = Request.Query["invoiceCode"];
					HttpContext.Session.SetObject<List<StoreEntity>>("storesUpdateInvoice", newStoreUpdate);
					return RedirectToPage("./UpdateInvoice", new { continueSession = true, invoiceCode = invoiceCode });
				}
				else
				{
					result = "Row is InValid!!";
					return Page();
				}
			}

			return Page();
		}
    }
}
