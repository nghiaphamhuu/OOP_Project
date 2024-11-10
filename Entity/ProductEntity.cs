using System.Globalization;
using WebApplication2.DAO;
using WebApplication2.Service;

namespace WebApplication2.Entity
{
	public class ProductEntity
	{
		public int id { get; set; }
		public string name { get; set; }
		public string date { get; set; }
		public string company { get; set; }
		public string dateOfProduce { get; set; }
		public string type { get; set; }
		public int price { get; set; }

		public ProductEntity(string name, string date, string company, string dateOfProduce, string type, string price)
		{
			if (string.IsNullOrEmpty(price))
			{
				throw new Exception("Please Input Price Of Product!");
			}

			if (!int.TryParse(price, out int x))
			{
				throw new Exception("Price is InValid!!");
			}
			else
			{
				if (int.Parse(price) <= 0)
				{
					throw new Exception("Price must be greater than 0");
				}
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Please Input Name Of Product!");
			}

			if (string.IsNullOrEmpty(date))
			{
				throw new Exception("Please Input Date Of Product!");
			}
			else
			{
				if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime err))
				{
					throw new Exception("Date of Product is inValid!");
				}
			}

			if (string.IsNullOrEmpty(company))
			{
				throw new Exception("Please Input Name Of Company!");
			}

			if (string.IsNullOrEmpty(dateOfProduce))
			{
				throw new Exception("Please Input Date Of Produce!");
			}
			else
			{
				if (!DateTime.TryParseExact(dateOfProduce, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime err))
				{
					throw new Exception("Date Of Produce is inValid!");
				}
			}

			if (string.IsNullOrEmpty(type))
			{
				throw new Exception("Please Input Type Of Product!");
			}

			CategoryService categoryService = new CategoryService(new CategoryDAO());
			List<CategoryEntity> listCheck = categoryService.getAllCategory();
			bool chk = true;

			foreach (CategoryEntity cate in listCheck)
			{
				if (cate.typeCd.Equals(type))
				{
					chk = false;
					break;
				}
			}

			if (chk || listCheck.Count <= 0)
			{
                throw new Exception("This Type of Product don't contains in Category, Please add this type in category or choice a item in that!");
            }

			this.name = name;
			this.date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture).ToString("dd/MM/yyyy");
			this.company = company;
			this.dateOfProduce = DateTime.ParseExact(dateOfProduce, "dd/MM/yyyy", CultureInfo.CurrentCulture).ToString("dd/MM/yyyy");
			this.type = type;
			this.price = int.Parse(price);
		}

		public ProductEntity()
		{
			
		}
	}
}
