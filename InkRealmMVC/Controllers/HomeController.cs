using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace InkRealmMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly InkRealmContext _context;

        public HomeController(InkRealmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           return await Task.Run(View);
        }
        [Authorize]
        public async Task<IActionResult> MasterIndex(InkMaster master) => await Task.Run(() => View(master));
        [Authorize]
        public async Task<IActionResult> ClientIndex(InkClient client) => await Task.Run(() => View(client));

        public async Task<IActionResult> Privacy() => await Task.Run(View);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error() => await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
    }
}