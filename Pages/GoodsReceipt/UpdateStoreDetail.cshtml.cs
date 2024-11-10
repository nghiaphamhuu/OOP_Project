using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;

namespace WebApplication2.Pages.GoodsReceipt
{
	public class UpdateStoreDetailModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public string row { get; set; }

		[BindProperty(SupportsGet = true)]
		public string goodsReceiptCode { get; set; }
		[BindProperty]
		public string result { get; set; }
		[BindProperty]
		public string quantity { get; set; }

		[BindProperty]
		public StoreEntity storeDetail { get; set; }

		[BindProperty]
		public List<StoreEntity> stores { get; set; }

		public void OnGet()
		{
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateGoodsReceipt");
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
			stores = HttpContext.Session.GetObject<List<StoreEntity>>("storesUpdateGoodsReceipt");
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
							update.quantity = int.Parse(quantity);
							newStoreUpdate.Add(update);
							continue;
						}
						newStoreUpdate.Add(stores[i]);
					}

					string goodsReceiptCode = Request.Query["goodsReceiptCode"];
					HttpContext.Session.SetObject<List<StoreEntity>>("storesUpdateGoodsReceipt", newStoreUpdate);
					return RedirectToPage("./UpdateGoodsReceipt", new { continueSession = true, goodsReceiptCode = goodsReceiptCode });
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
