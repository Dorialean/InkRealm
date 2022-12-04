using InkRealmMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class ClientAreaController : Controller
    {
        [Authorize(Roles = "Client")]
        public IActionResult Index() => View();   
    }
}
