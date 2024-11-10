using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Invoice
{
    public class DeleteInvoiceModel : PageModel
    {
        [BindProperty]
        public string result { get; set; }
        [BindProperty(SupportsGet = true)]
        public string invoiceCode { get; set; }
        [BindProperty]
        public List<StoreEntity> stores { get; set; }
        [BindProperty]
        public InvoiceEntity invoice { get; set; }

        private IInvoiceService _invoiceService;
        public DeleteInvoiceModel() : base()
        {
            _invoiceService = ObjectCreator.createInvoiceService();
        }
        public void OnGet()
        {
            invoice = _invoiceService.getInvoiceDetail(invoiceCode);
            stores = invoice.stores;
        }
        public IActionResult OnPost()
        {
            _invoiceService.deleteInvoice(invoiceCode, true);
            return RedirectToPage("./Index");
        }
    }
}
