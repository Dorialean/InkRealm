using InkRealmMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class ClientController : Controller
    {
        [Authorize(Roles = Role.InkClient)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
