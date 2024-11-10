using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class ProductDAO :IProductDAO
    {
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Product.txt";
        public List<ProductEntity> getAllProduct()
        {
            List<ProductEntity> products = new List<ProductEntity>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                ProductEntity product = JsonConvert.DeserializeObject<ProductEntity>(line);
                products.Add(product);
            }

            reader.Close();

            return products;
        }

        public void addProduct(ProductEntity product)
        {
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            int count = 1;
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }

            reader.Close();

            product.id = count;
            string json = JsonConvert.SerializeObject(product);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public ProductEntity getProductDetail(int id)
        {
            List<ProductEntity> products = getAllProduct();
            return products.FirstOrDefault(x => x.id == id);
        }

        public List<ProductEntity> getProductBySearchName(string keySearch)
        {
            List<ProductEntity> products = getAllProduct();
            List<ProductEntity> result = new List<ProductEntity>();

            foreach(ProductEntity item in products)
            {
                if(item.name.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public void updateProduct(ProductEntity product)
        {
            List<ProductEntity> products = getAllProduct();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (ProductEntity item in products)
            {
                string json = JsonConvert.SerializeObject(item);

                if (product.id == item.id)
                {
                    json = JsonConvert.SerializeObject(product);
                }
               
                writer.WriteLine(json);
            }

            writer.Close();
        }

        public void deleteProduct(int id)
        {
            List<ProductEntity> products = getAllProduct();

            StreamWriter writer = new StreamWriter(filePath);
            int idx = 0;

            foreach(ProductEntity product in products)
            {
                if (product.id == id)
                {
                    continue;
                }

                ProductEntity product2 = product;
                product2.id = idx;
                idx++;

                string json = JsonConvert.SerializeObject(product2);

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public List<ProductEntity> getProductsByCategory(string category)
        {
            List<ProductEntity> list = getAllProduct();
            List<ProductEntity> result = new List<ProductEntity>();

            foreach(ProductEntity item in list)
            {
                if(item.type.Equals(category))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
