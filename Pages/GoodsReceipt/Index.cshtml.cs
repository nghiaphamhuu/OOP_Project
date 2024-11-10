using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Entity;
using WebApplication2.Service;

namespace WebApplication2.Pages.GoodsReceipt
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public List<GoodsReceiptEntity> goodsReceipts { get; set; }

        [BindProperty]
        public string keySearch { get; set; }
        private IGoodsReceiptService _goodsReceiptService;

        public IndexModel() : base()
        {
            _goodsReceiptService = ObjectCreator.createGoodsReceiptService();
        }
        public void OnGet()
        {
            goodsReceipts = _goodsReceiptService.getAllGoodsReceipt();
        }

        public void OnPost()
        {
            goodsReceipts = _goodsReceiptService.getGoodsReceiptBySearchName(keySearch);
        }
    }
}
