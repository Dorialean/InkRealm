using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InkRealmMVC.Controllers
{
    [Authorize(Roles = Role.InkClient)]
    public class ClientController : Controller
    {
        private readonly InkRealmContext _context;

        public ClientController(InkRealmContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (_context)
            {
                InkClient inkClient = await _context.InkClients.FirstAsync(c => c.Login == User.Identity.Name);
                List<Order> orders = await _context.Orders.Select(o => o).Where(o => o.ClientId == inkClient.ClientId).ToListAsync();
                List<InkClientService> clientServices = await _context.InkClientServices.Select(s => s).Where(s => s.ClientId == inkClient.ClientId).ToListAsync();
                List<InkService> neededServices = (from cServ in clientServices join aServ in await _context.InkServices.ToListAsync()
                                                   on cServ.ServiceId equals aServ.ServiceId
                                                   where cServ.ClientId == inkClient.ClientId
                                                   select aServ).ToList();
                List<InkMaster> mastersComeTo = (from cServ in clientServices join mast in await _context.InkMasters.ToListAsync()
                                                 on cServ.MasterId equals mast.MasterId
                                                 where cServ.ClientId == inkClient.ClientId
                                                 select mast).ToList();

                List<MasterReviews> reviews = await _context.MasterReviews.Select(r => r).Where(r => r.ClientId == inkClient.ClientId).ToListAsync();
                List<InkProduct> orederedProducts = (from prod in await _context.InkProducts.ToListAsync() join ord in await _context.Orders.ToListAsync()
                                                      on prod.ProductId equals ord.ProductId
                                                     where ord.ClientId == inkClient.ClientId
                                                     select prod).ToList();
                                                  

                return await Task.Run(() =>  View(new ClientSpaceModel()
                {
                    Client = inkClient,
                    NeededServices = neededServices,
                    MastersToCome = mastersComeTo,
                    ClientServices = clientServices,
                    ClientReviewd = reviews,
                    OrderedProducts = orederedProducts
                }));
            }
        }

        
    }
}
