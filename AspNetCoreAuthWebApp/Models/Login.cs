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
    public enum Roles
    {
        Admin,
        User
    }
}
