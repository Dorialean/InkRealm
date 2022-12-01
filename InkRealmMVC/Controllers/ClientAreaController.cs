using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class ClientAreaController : Controller
    {
        [Authorize]
        public IActionResult ClientArea()
        {
            return View();
        }   
    }
}
