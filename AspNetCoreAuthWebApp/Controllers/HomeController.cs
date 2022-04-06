using AspNetCoreAuthWebApp.Models;
using AspNetCoreAuthWebApp.Repositories;
using AspNetCoreAuthWebApp.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 

namespace AspNetCoreAuthWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IToastifyService _notifyService { get; }
        private readonly ILoginRepository _login;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private LoginDto GetUser(Login login)
        {
            return _login.GetUser(login);
        }
        public HomeController(ILoginRepository login, IConfiguration configuration, ITokenService tokenService, IToastifyService notifyService)
        {
            _login = login;
            _configuration =configuration;
            _tokenService = tokenService;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return (RedirectToAction("Error"));
            }

            var validUser = GetUser(login);

            if (validUser != null)
            {
               var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(),
                validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    _notifyService.Success("Giriş Başarılı");
                    return RedirectToAction("Login");
                }
                else
                {
                    return (RedirectToAction("Index"));
                }
            }
            else
            {
                _notifyService.Error("Giriş Başarısız.Bilgilerin doğruluğundan emin olunuz.");
                return (RedirectToAction("Index"));
            }
            
        }
        [Authorize]
        [Route("Login")]
        [HttpGet]
        public IActionResult Page()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index"));
            }

            if (!_tokenService.IsTokenValid(_configuration["Jwt:Key"].ToString(),
                _configuration["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Index"));
            }
             
            return View();
        }
        public IActionResult Error()
        {
            ViewBag.Message = "Bir hata oluştu.Tekrar deneyiniz...";
            return View();
        }
    }
}
