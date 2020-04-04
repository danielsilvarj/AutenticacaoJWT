using AutenticacaoJWT.Models;
using System.Collections.Generic;
using System.Linq;


namespace AutenticacaoJWT.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            List<User> users = new List<User>();
            users.Add(new User { Id = 1, UserName = "Daniel", Password = "Daniel", Role = "Lider" });
            users.Add(new User { Id = 1, UserName = "Dan", Password = "Dan", Role = "Membro" });
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
