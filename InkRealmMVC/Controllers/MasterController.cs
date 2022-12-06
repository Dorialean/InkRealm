using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkRealmMVC.Controllers
{
    public class MasterController : Controller
    {
        private readonly InkRealmContext _context;

        public MasterController(InkRealmContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Role.InkWorker)]
        [HttpGet]
        public IActionResult Index()
        {
            InkMaster master;
            List<MastersServices> services = new();
            List<MastersSupply> supplies = new();
            List<ClientsNeed> mastersWork = new();
            Studio studio;

            using (_context)
            {
                master = _context.InkMasters.First(m => m.Login == User.Identity.Name);
                studio = _context.Studios.Find(master.StudioId);
                services = _context.MastersServices.Select(s => s).Where(s => s.MasterId == master.MasterId).ToList();
                supplies = _context.MastersSupplies.Select(s => s).Where(s => s.MasterId == master.MasterId).ToList();
                mastersWork = _context.ClientsNeeds.Select(n => n).Where(n => n.MasterId == master.MasterId).ToList();

                return View(new MasterSpaceModel()
                {
                    Master = master,
                    MasterStudio = studio,
                    MasterServices = services,
                    MastersWorks = mastersWork,
                    MastersSupplies = supplies
                });
            }
        }
    }
}
