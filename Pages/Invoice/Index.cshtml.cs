using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Invoice
{
    public class IndexModel : PageModel
    {
		[BindProperty]
		public string keySearch { get; set; }

		[BindProperty]
		public List<InvoiceEntity> invoices { get; set; }

		private IInvoiceService _invoiceService;
		public IndexModel() : base()
		{
			_invoiceService = ObjectCreator.createInvoiceService();
		}
		public void OnGet()
        {
			invoices = _invoiceService.getInvoiceBySearchName(keySearch == null ? "" : keySearch);
		}
    }
}
