using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.Invoice
{
    public class AddInvoiceModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string selectedProductId { get; set; }

		[BindProperty(SupportsGet = true)]
		public string quantityForm { get; set; }

		[BindProperty(SupportsGet = true)]
		public string keepSession { get; set; }

		[BindProperty]
		public string result { get; set; }
		[BindProperty]
		public string invoiceCode { get; set; }
		[BindProperty]
		public string insertDate { get; set; }
		[BindProperty]
		public List<StoreEntity> stores { get; set; }

		[BindProperty]
		public List<ProductEntity> products { get; set; }

		[BindProperty]
		public InvoiceEntity invoice { get; set; }

		private IProductService _productService;
		private IInvoiceService _invoiceService;
        private IStoreService _storeService;
        public AddInvoiceModel() : base()
		{
			_productService = ObjectCreator.createProductService();
			_invoiceService = ObjectCreator.createInvoiceService();
            _storeService = ObjectCreator.createStoreService();

        }
		public void OnGet()
        {
			invoice = new InvoiceEntity();
			products = _productService.getAllProduct();

			if (!"true".Equals(keepSession))
			{
				HttpContext.Session.Remove("storesInvoice");
			}

			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesInvoice");
			if(stores == null)
			{
				stores = new List<StoreEntity>();
			}

            if (!string.IsNullOrEmpty(selectedProductId)
            && !string.IsNullOrEmpty(quantityForm))
            {
                if (int.TryParse(selectedProductId, out int err)
                    && int.TryParse(quantityForm, out int err1))
                {
                    int id = int.Parse(selectedProductId);
                    int quantity = int.Parse(quantityForm);
                    addStore(id, quantity);
                }
                else
                {
                    result = "Quantity or Id is Invalid!";
                }
            }
            else
            {
                result = "Please Input Quantity and select product";
            }
        }
        public void addStore(int id, int quanity)
        {
            Entity.ProductEntity product = _productService.getProductDetail(id);
            StoreEntity store = new StoreEntity();
            store.productId = product.id;
            store.productName = product.name;
            store.typeCd = product.type;
            store.quantity = quanity;
            store.price = product.price;
            store.total = quanity * product.price;
            stores.Add(store);
            HttpContext.Session.SetObject("storesInvoice",stores);
        }

		public IActionResult OnPost()
		{
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesInvoice");
			if (stores == null)
			{
				stores = new List<StoreEntity>();
			}

			List<InvoiceEntity> listCheck = _invoiceService.getAllInvoice();

			foreach (InvoiceEntity inv in listCheck)
			{
				if (inv.invoiceCode.Equals(invoiceCode))
				{
					result = "This Goods Receipt Code is already Exist, Please Input another Goods Receipt Code!";
					return Page();
				}
			}

			List<StoreEntity> checkStore = _storeService.getAllStore();

			foreach (StoreEntity item in stores)
			{
				bool chkExistStore = false;
				bool chkQuantity = true;
				int quantity = 0;
				foreach (StoreEntity chk in checkStore)
				{
					if (chk.productId == item.productId)
					{
						if (chk.quantity < item.quantity)
						{
							chkQuantity = false;
							quantity = chk.quantity;
							break;
						}
						chkExistStore = true;
						break;
					}
				}

				if (!chkQuantity)
				{
					result = item.productName + " ,The Quantity of this Product is greater than Store(" + quantity + ")";
					return Page();
				}

				if (!chkExistStore)
				{
					result = item.productName + " ,This Product is not exist in Store!!";
					return Page();
				}
			}
			try
			{
				invoice = new InvoiceEntity(invoiceCode, stores);
				invoice.insertDate = DateTime.Now.ToString("dd/MM/yyyy");
				_invoiceService.addInvoice(invoice, true);
				result = "Add Success";
				HttpContext.Session.Remove("storesInvoice");
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
