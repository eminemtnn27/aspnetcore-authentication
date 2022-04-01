using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthWebApp.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
    //public readonly List<Login> user =
    //        new List<Login> {
    //            new Login { Username = "Zeynep", Password = "123" },
    //            new Login { Username = "Görkem", Password = "1234" },
    //            new Login { Username = "Emine", Password = "12345", isAdmin = true}
    //        };
    public enum Roles
    {
        Admin,
        User
    }
}
