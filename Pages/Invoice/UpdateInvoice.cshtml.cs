using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Invoice
{
    public class UpdateInvoiceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string continueSession {  get; set; }
		[BindProperty(SupportsGet = true)]
		public string invoiceCode { get; set; }
		[BindProperty]
		public string insertDate { get; set; }
		[BindProperty]
		public List<StoreEntity> stores { get; set; }
		[BindProperty]
		public InvoiceEntity invoice { get; set; }
		[BindProperty]
		public string result { get; set; }
		private IInvoiceService _invoiceService;
		public UpdateInvoiceModel() : base()
		{
			_invoiceService = ObjectCreator.createInvoiceService();
		}

		public void OnGet()
        {
			if (!"true".Equals(continueSession))
			{
				HttpContext.Session.Remove("storesUpdateInvoice");
			}

			invoice = HttpContext.Session.GetObject<InvoiceEntity>("invoiceDetail");
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateInvoice");
			invoice = _invoiceService.getInvoiceDetail(invoiceCode);

			if (stores == null)
			{
				HttpContext.Session.SetObject<InvoiceEntity>("invoiceDetail", invoice);
				HttpContext.Session.SetObject<List<StoreEntity>>("storesUpdateInvoice", invoice.stores);
				stores = invoice.stores;
			}
		}

		public IActionResult OnPost()
		{
			try
			{
				invoice.invoiceCode = invoiceCode;
				invoice.insertDate = insertDate;
				invoice.stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateInvoice");
				int check = _invoiceService.checkValid(invoice);
				if (check > -1)
				{
					throw new Exception(stores[check].productName + " ,The quantity of goods in stock is not enough to ship!");
				}

				_invoiceService.updateInvoice(invoice);
				HttpContext.Session.Remove("storesUpdateInvoice");
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
