using AspNetCoreAuthWebApp.Helper;
using AspNetCoreAuthWebApp.Models;
using AspNetCoreAuthWebApp.Repositories;
using AspNetCoreAuthWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 

namespace AspNetCoreAuthWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginRepository _login;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private LoginDto GetUser(Login login)
        {
            return _login.GetUser(login);
        }
        public HomeController(ILoginRepository login, IConfiguration configuration, ITokenService tokenService)
        {
            _login = login;
            _configuration =configuration;
            _tokenService = tokenService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return (RedirectToAction("Error"));
            }

            IActionResult response = Unauthorized();
            var validUser = GetUser(login);

            if (validUser != null)
            {
               var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(),
                validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("Home");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
            
        }

        public IActionResult Error()
        {
            ViewBag.Message = "Bir hata oluştu...";
            return View();
        }
    }
}
