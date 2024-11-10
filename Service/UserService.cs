using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class UserService
    {
        public static List<UserEntity> getAllUser()
        {
            return UserDAO.getAllUser();
        }

        public static string createUser(UserEntity user)
        {
            List<UserEntity> users = getAllUser();   
            
            foreach(UserEntity item in users)
            {
                if (item.email.Equals(user.email))
                {
                    return "The Email was registered ";
                }
            }

            UserDAO.createUser(user);

            return "Create User Successfully!";
        }

        public static bool logIn(UserEntity user)
        {
            List<UserEntity> users = getAllUser();

            foreach(UserEntity item in users)
            {
                if(item.email.Equals(user.email))
                {
                    if (item.password.Equals(user.password)) 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
