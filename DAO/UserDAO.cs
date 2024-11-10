using Newtonsoft.Json;
using System;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public class UserDAO
	{
		private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\User.txt";
        public static void createUser(UserEntity user)
		{
			string json = JsonConvert.SerializeObject(user);

			StreamWriter writer = new StreamWriter(filePath,append: true);
			writer.WriteLine(json);
			writer.Close();
        }


		public static List<UserEntity> getAllUser()
		{
			List<UserEntity> users = new List<UserEntity>();
			StreamReader reader = new StreamReader(filePath);
			string line = null;
			while ((line = reader.ReadLine()) != null)
			{
				UserEntity user = JsonConvert.DeserializeObject<UserEntity>(line);
				users.Add(user);
			}

			reader.Close();

			return users;
		}
    }
}
