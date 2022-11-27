using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly InkRealmContext _context;

        public AuthController(InkRealmContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> MasterRegister() => await Task.Run(View);
        
        [HttpGet]
        public async Task<IActionResult> ClientRegister() => await Task.Run(View);

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

        public IActionResult Logout()
        {
            return null;
        }
    }
}
