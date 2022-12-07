using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace InkRealmMVC.Controllers
{
    public class MasterController : Controller
    {
        private readonly InkRealmContext _context;

        const string MASTER_PICTURE_WORK_PATH = "wwwroot/img/masters_img/works";
        

        public MasterController(InkRealmContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Role.InkWorker)]
        [HttpGet]
        public IActionResult Index()
        {
            InkMaster master;
            List<InkService> services = new();
            List<InkSupply> supplies = new();
            List<ClientsNeed> mastersWork = new();
            Studio studio;

            using (_context)
            {
                master = _context.InkMasters.First(m => m.Login == User.Identity.Name);
                studio = _context.Studios.Find(master.StudioId);
                services = (from mServs in _context.MastersServices.ToList() join servs in _context.InkServices.ToList() 
                                on mServs.ServiceId equals servs.ServiceId
                           select servs).ToList(); 
                supplies = (from mSupl in _context.MastersSupplies.ToList() join supl in _context.InkSupplies.ToList()
                                on mSupl.SuplId equals supl.SuplId
                            select supl).ToList();
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
