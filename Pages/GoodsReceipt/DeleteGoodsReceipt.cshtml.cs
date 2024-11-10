using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.GoodsReceipt
{
    public class DeleteGoodsReceiptModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string goodsReceiptCode { get; set; }

		[BindProperty]
		public string insertDate { get; set; }

		[BindProperty]
		public List<StoreEntity> stores { get; set; }
		[BindProperty]
		public GoodsReceiptEntity goodsReceipt { get; set; }
		private IGoodsReceiptService _goodsReceiptService;
		public DeleteGoodsReceiptModel() : base()
		{
			_goodsReceiptService = ObjectCreator.createGoodsReceiptService();
		}
		public void OnGet()
        {
			goodsReceipt = _goodsReceiptService.getGoodsReceiptDetail(goodsReceiptCode);
			stores = goodsReceipt.stores;
		}

		public IActionResult OnPost()
		{
			_goodsReceiptService.deleteGoodsReceipt(goodsReceiptCode, true);

			return RedirectToPage("./Index");
		}
    }
}
