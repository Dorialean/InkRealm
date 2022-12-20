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
                List<InkService> clientServices = new();
                List<InkMaster> mastersComeTo = new();
                List<MasterReviews> reviews = _context.MasterReviews.Select(r => r).Where(r => r.ClientId == inkClient.ClientId).ToList();
                List<InkProduct> orederedProducts = (from prod in _context.InkProducts.ToList() join ord in _context.Orders.ToList()
                                                      on prod.ProductId equals ord.ProductId
                                                     where ord.ClientId == inkClient.ClientId
                                                     select prod).ToList();
                                                  

                return View(new ClientSpaceModel()
                {
                    Client = inkClient,
                    Orders = orders,
                    ClientServices = clientServices,
                    MastersToCome = mastersComeTo,
                    ClientReviewd = reviews,
                    OrderedProducts = orederedProducts
                });
            }
        }
    }
}
