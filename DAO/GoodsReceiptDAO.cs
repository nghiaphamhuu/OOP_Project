using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class GoodsReceiptDAO : IGoodsReceiptDAO
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\GoodsReceipt.txt";
        public List<GoodsReceiptEntity> getAllGoodsReceipt()
        {
            List<GoodsReceiptEntity> goodsReceipts = new List<GoodsReceiptEntity>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                GoodsReceiptEntity goodsReceipt = JsonConvert.DeserializeObject<GoodsReceiptEntity>(line);
                goodsReceipts.Add(goodsReceipt);
            }

            reader.Close();

            return goodsReceipts;
        }

        public void addGoodsReceipt(GoodsReceiptEntity goodsReceipt)
        {
            string json = JsonConvert.SerializeObject(goodsReceipt);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public GoodsReceiptEntity getGoodsReceiptDetail(string goodsReceiptCode)
        {
            List<GoodsReceiptEntity> goodsReceipts = getAllGoodsReceipt();
            return goodsReceipts.FirstOrDefault(x => x.goodsReceiptCode.Equals(goodsReceiptCode));
        }

        public List<GoodsReceiptEntity> getGoodsReceiptBySearchName(string keySearch)
        {
            List<GoodsReceiptEntity> goodsReceipts = getAllGoodsReceipt();
            List<GoodsReceiptEntity> result = new List<GoodsReceiptEntity>();

            foreach (GoodsReceiptEntity item in goodsReceipts)
            {
                if (item.goodsReceiptCode.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public void updateGoodsReceipt(GoodsReceiptEntity goodsReceipt)
        {
            List<GoodsReceiptEntity> goodsReceipts = getAllGoodsReceipt();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (GoodsReceiptEntity item in goodsReceipts)
            {
                string json = JsonConvert.SerializeObject(item);

                if (goodsReceipt.goodsReceiptCode.Equals(item.goodsReceiptCode))
                {
                    json = JsonConvert.SerializeObject(goodsReceipt);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public void deleteGoodsReceipt(string goodsReceiptCode)
        {
            List<GoodsReceiptEntity> goodsReceipts = getAllGoodsReceipt();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (GoodsReceiptEntity goodsReceipt in goodsReceipts)
            {
                if (goodsReceipt.goodsReceiptCode.Equals(goodsReceiptCode))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(goodsReceipt);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
