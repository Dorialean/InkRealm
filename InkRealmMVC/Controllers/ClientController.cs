using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InkRealmMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly InkRealmContext _context;

        public ClientController(InkRealmContext context)
        {
            _context = context; 
        }

        [Authorize(Roles = Role.InkClient)]
        [HttpGet]
        public IActionResult Index()
        {
            using (_context)
            {
                InkClient inkClient = _context.InkClients.First(c => c.Login == User.Identity.Name);
                List<Order> orders = _context.Orders.Select(o => o).Where(o => o.ClientId == inkClient.ClientId).ToList();
                List<ClientsNeed> clientNeeds = _context.ClientsNeeds.Select(n => n).Where(n => n.ClientId == inkClient.ClientId).ToList();
                List<InkService> clientServices = (from inkServ in _context.InkServices.ToList()
                                                   join cNeeds in _context.ClientsNeeds.ToList()
                                                    on inkServ.ServiceId equals cNeeds.ServiceId
                                                   select inkServ).ToList();
                List<InkMaster> mastersComeTo = (from master in _context.InkMasters.ToList()
                                                 join needs in _context.ClientsNeeds.ToList() 
                                                    on master.MasterId equals needs.MasterId
                                                 select master).ToList();
                List<MasterReviews> reviews = _context.MasterReviews.Select(r => r).Where(r => r.ClientId == inkClient.ClientId).ToList();
                List<InkProduct> orederedProducts = (from prod in _context.InkProducts.ToList() join ord in _context.Orders.ToList()
                                                      on prod.ProductId equals ord.ProductId
                                                     select prod).ToList();
                                                  

                return View(new ClientSpaceModel()
                {
                    Client = inkClient,
                    Orders = orders,
                    ClientNeeds = clientNeeds,
                    ClientServices = clientServices,
                    MastersToCome = mastersComeTo,
                    ClientReviewd = reviews,
                    OrderedProducts = orederedProducts
                });
            }
        }
    }
}
