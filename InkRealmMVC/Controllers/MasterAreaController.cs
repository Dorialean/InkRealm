using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class MasterAreaController : Controller
    {
        [Authorize(Roles = "Worker")]
        public IActionResult Index() => View();
    }
}
