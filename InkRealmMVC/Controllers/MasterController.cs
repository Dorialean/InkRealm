using InkRealmMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class MasterController : Controller
    {
        [Authorize(Roles = Role.InkWorker)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
