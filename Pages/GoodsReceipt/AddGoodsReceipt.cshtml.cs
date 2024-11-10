using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.GoodsReceipt
{
    public class AddGoodsReceiptModel : PageModel
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
        public string goodsReceiptCode { get; set; }
        [BindProperty]
        public string insertDate { get; set; }
        [BindProperty]
        public List<StoreEntity> stores { get; set; }

        [BindProperty]
        public List<ProductEntity> products { get; set; }

        [BindProperty]
        public GoodsReceiptEntity goodsReceipt { get; set; }

        private IProductService _productService;
        private IGoodsReceiptService _goodsReceiptService;
        public AddGoodsReceiptModel() : base()
        {
            _productService = ObjectCreator.createProductService();
            _goodsReceiptService = ObjectCreator.createGoodsReceiptService();
        }
        public void OnGet()
        {
            goodsReceipt = new GoodsReceiptEntity();
            products = _productService.getAllProduct();

            if (!"true".Equals(keepSession))
            {
                HttpContext.Session.Remove("storesGoodsReceipt");
            }

            stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesGoodsReceipt");
            if (stores == null)
            {
                stores = new List<StoreEntity>();
            }
            if (!string.IsNullOrEmpty(selectedProductId)
            && !string.IsNullOrEmpty(quantityForm))
            {
                goodsReceipt = new GoodsReceiptEntity();
                goodsReceipt.insertDate = DateTime.Now.ToString("dd/MM/yyyy");

                products = _productService.getAllProduct();

                if (int.TryParse(selectedProductId, out int err)
                    && int.TryParse(quantityForm, out int err1))
                {
                    int id = int.Parse(Request.Query["selectedProductId"]);
                    int quantityForm = int.Parse(Request.Query["quantityForm"]);
                    addStore(id, quantityForm);
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
            ProductEntity product = _productService.getProductDetail(id);
            StoreEntity store = new StoreEntity();
            store.productId = product.id;
            store.productName = product.name;
            store.typeCd = product.type;
            store.quantity = quanity;
            store.price = product.price;
            store.total = quanity*product.price;
            stores.Add(store);
            HttpContext.Session.SetObject("storesGoodsReceipt", stores);
        }

        public IActionResult OnPost()
        {
            try
            {
				stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesGoodsReceipt");
				if (stores == null)
				{
					stores = new List<StoreEntity>();
				}
				goodsReceipt = new GoodsReceiptEntity(goodsReceiptCode,stores);
                _goodsReceiptService.addGoodsReceipt(goodsReceipt, true);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            result = "Add Success";
            return RedirectToPage("./Index");
            
        }
    }
}
