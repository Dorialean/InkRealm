using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace InkRealmMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly InkRealmContext _context;

        public AuthController(InkRealmContext context) => _context = context;

        //TODO:Здесь хардкод, надо будет получать из ENUM в БД
        [HttpGet]
        public async Task<IActionResult> MasterRegister()
        {
            List<Studio> allStudios;
            List<InkService> allInkServices;
            using (_context)
            {
                allStudios = _context.Studios.ToList();
                allInkServices = _context.InkServices.ToList();   
            }
            return await Task.Run(() => View(new MasterRegister()
            {
                AllServices = allInkServices,
                AllStudios = allStudios,
                AllProfs = new() { "sketch designer", "tatoo master", "pirsing master" }
            }));
        }
        [HttpPost]
        public IActionResult MasterRegister(MasterRegister master)
        {
            if (!IsValidModel(master))
                return BadRequest("Введены не все данные");

            using (_context)
            {
                if (_context.InkMasters.First(m => m.Login == master.Login) != null)
                    return BadRequest("Пользователь с таким логином уже существует");


            }

            return RedirectToAction("MasterArea", "MasterAreaController");
        }
        
        [HttpGet]
        public async Task<IActionResult> ClientRegister() => await Task.Run(View);
        [HttpPost]
        public IActionResult ClientRegister(InkClient client)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()  => await Task.Run(View);

        [HttpPost]
        public IActionResult Login(LoginModel loginInfo)
        {
            using (_context)
            {
                if (_context.InkMasters.First(m => m.Login == loginInfo.Login) != null)
                {
                    //Method
                }
                else if(_context.InkClients.First(c => c.Login == loginInfo.Login) != null)
                {
                    //Method
                }
                else
                {
                    return RedirectToAction("ClientRegister", "Auth");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await ControllerContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }

        private static bool IsValidModel(MasterRegister master)
        {
            if (string.IsNullOrEmpty(master.Login))
                return false;
            if (string.IsNullOrEmpty(master.Password))
                return false;
            if (string.IsNullOrEmpty(master.FirstName))
                return false;
            if (string.IsNullOrEmpty(master.SecondName))
                return false;
            if (master.ServicesTitles.Count == 0)
                return false;
            if (string.IsNullOrEmpty(master.StudioAddress))
                return false;
            return true;
        }
        
        private async Task RegisterNewUser(MasterRegister master)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, master.Login),
                new Claim(ClaimTypes.Role, master.InkPost)
            
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
