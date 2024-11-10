using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Category.txt";
        public List<CategoryEntity> getAllCategory()
        {
            List<CategoryEntity> categorys = new List<CategoryEntity>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                CategoryEntity category = JsonConvert.DeserializeObject<CategoryEntity>(line);
                categorys.Add(category);
            }

            reader.Close();

            return categorys;
        }

        public void addCategory(CategoryEntity category)
        {
            StreamReader reader = new StreamReader(filePath);

            reader.Close();

            string json = JsonConvert.SerializeObject(category);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public CategoryEntity getCategoryDetail(string typeCd)
        {
            List<CategoryEntity> categorys = getAllCategory();
            return categorys.FirstOrDefault(x => x.typeCd.Equals(typeCd));
        }

        public List<CategoryEntity> getCategoryBySearchName(string keySearch)
        {
            List<CategoryEntity> categorys = getAllCategory();
            List<CategoryEntity> result = new List<CategoryEntity>();

            foreach (CategoryEntity item in categorys)
            {
                if (item.typeDesc.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public void updateCategory(CategoryEntity category)
        {
            List<CategoryEntity> categorys = getAllCategory();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (CategoryEntity item in categorys)
            {
                string json = JsonConvert.SerializeObject(item);

                if (category.typeCd.Equals(item.typeCd))
                {
                    json = JsonConvert.SerializeObject(category);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public void deleteCategory(string typeCd)
        {
            List<CategoryEntity> categorys = getAllCategory();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (CategoryEntity category in categorys)
            {
                if (category.typeCd.Equals(typeCd))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(category);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
