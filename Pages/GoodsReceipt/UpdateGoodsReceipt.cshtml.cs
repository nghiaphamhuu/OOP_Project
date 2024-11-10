using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.GoodsReceipt
{
	public class UpdateGoodsReceiptModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string continueSession { get; set; }

		[BindProperty(SupportsGet = true)]
		public string goodsReceiptCode { get; set; }

		[BindProperty]
		public GoodsReceiptEntity goodsReceipt { get; set; }

		[BindProperty]
		public string insertDate { get; set; }

		[BindProperty]
		public List<StoreEntity> stores { get; set; }
		private IGoodsReceiptService _goodsReceiptService;
		public UpdateGoodsReceiptModel() : base()
		{
			_goodsReceiptService = ObjectCreator.createGoodsReceiptService();
		}
		public void OnGet()
		{
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateGoodsReceipt");
			goodsReceipt = _goodsReceiptService.getGoodsReceiptDetail(goodsReceiptCode);
			goodsReceipt.insertDate = insertDate;
			if (stores == null)
			{
				HttpContext.Session.SetObject<GoodsReceiptEntity>("goodReceiptDetail", goodsReceipt);
				HttpContext.Session.SetObject<List<StoreEntity>>("storesUpdateGoodsReceipt", goodsReceipt.stores);
				stores = goodsReceipt.stores;
			}
		}

		public IActionResult OnPost()
		{
			try
			{
				stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateGoodsReceipt");
				GoodsReceiptEntity goodsReceipt = new GoodsReceiptEntity(goodsReceiptCode, stores);
				_goodsReceiptService.updateGoodsReceipt(goodsReceipt);
			}
			catch (Exception ex)
			{
				
			}
			
			HttpContext.Session.Remove("storesUpdateGoodsReceipt");
			return RedirectToPage("./Index");
		}
    }
}
