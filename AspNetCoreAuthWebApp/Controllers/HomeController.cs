using AspNetCoreAuthWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthWebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly JwtHeader _jwtHelper;
        private readonly IHttpContextAccessor _context;
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
