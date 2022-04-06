using AspNetCoreAuthWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthWebApp.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly List<LoginDto> users = new List<LoginDto>();
        public LoginRepository()
        {
            users.Add(new LoginDto { UserName = "emine", Password = "deneme123", Role = "admin" });
            users.Add(new LoginDto { UserName = "zeynep", Password = "zey32", Role = "developer" });
            users.Add(new LoginDto { UserName = "tuba", Password = "3456", Role = "tester" }); 
        }
        public LoginDto GetUser(Login user)
        {
            return users.Where(x => x.UserName.ToLower() == user.UserName.ToLower()
                && x.Password == user.Password).FirstOrDefault();
        }
    }
}
