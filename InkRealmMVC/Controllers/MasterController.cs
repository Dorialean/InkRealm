using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

namespace InkRealmMVC.Controllers
{
    [Authorize(Roles = Role.InkWorker)]
    public class MasterController : Controller
    {
        private readonly InkRealmContext _context;

        const string MASTER_PICTURE_INFO_PATH = "wwwroot/img/masters_img/info";
        const string STUDIO_PICTURE_PATH = "wwwroot/img/studios_img";
        const string PRODUCT_PICTURE_PATH = "wwwroot/img/prod_img";
        

        public MasterController(InkRealmContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            InkMaster master;
            List<InkService> services = new();
            List<InkSupply> supplies = new();
            Studio studio;

            using (_context)
            {
                master = _context.InkMasters.First(m => m.Login == User.Identity.Name);
                studio = _context.Studios.Find(master.StudioId);
                services = (from mServs in _context.MastersServices.ToList() join servs in _context.InkServices.ToList() 
                                on mServs.ServiceId equals servs.ServiceId
                            where mServs.MasterId == master.MasterId
                           select servs).ToList(); 
                supplies = (from mSupl in _context.MastersSupplies.ToList() join supl in _context.InkSupplies.ToList()
                                on mSupl.SuplId equals supl.SuplId
                            where mSupl.MasterId == master.MasterId
                            select supl).ToList();

                List<InkClientService> masterServices = _context.InkClientServices.Select(m => m).Where(s => s.MasterId == master.MasterId).ToList();
                List<InkService> neededServices = (from mServ in masterServices
                                                   join aServ in _context.InkServices.ToList()
                                                   on mServ.ServiceId equals aServ.ServiceId
                                                   where mServ.MasterId == master.MasterId
                                                   select aServ).ToList();
                List<InkClient> mastersClient = (from mServ in masterServices
                                                 join aClient in _context.InkClients.ToList()
                                                 on mServ.ClientId equals aClient.ClientId
                                                 where mServ.MasterId == master.MasterId
                                                 select aClient).ToList();
                return View(new MasterSpaceModel()
                {
                    Master = master,
                    MasterStudio = studio,
                    MasterServices = services,
                    MasterClients = mastersClient,
                    MastersSupplies = supplies,
                    NeededServices = neededServices,
                    MastersServiceWork = masterServices,
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddNewStudio() => await Task.Run(View);

        [HttpPost]
        public async Task<IActionResult> AddNewStudio(StudioAppend studio)
        {
            using (_context)
            {
                Studio studioToAdd = new()
                {
                    Address = studio.Address,
                    RentalPricePerMonth = studio.RentalPricePerMonth
                };

                if (studio.Photo != null)
                {
                    var file = studio.Photo;
                    studioToAdd.PhotoLink = await AddPictureAsync(file, STUDIO_PICTURE_PATH);
                }

                await _context.Studios.AddAsync(studioToAdd);
                await _context.SaveChangesAsync();
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }

        [HttpGet]
        public async Task<IActionResult> AddNewProduct() => await Task.Run(View);

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductAppend product)
        {
            using (_context)
            {
                InkProduct productToAdd = new()
                {
                    Title = product.Title,
                    Description = product.Description,
                    EachPrice = product.EachPrice
                };

                if (product.Photo != null)
                {
                    var file = product.Photo;
                    productToAdd.PhotoLink = await AddPictureAsync(file, PRODUCT_PICTURE_PATH);
                }

                await _context.InkProducts.AddAsync(productToAdd);
                await _context.SaveChangesAsync();
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }

        [HttpGet]
        public async Task<IActionResult> AddNewService() => await Task.Run(View);

        [HttpPost]
        public async Task<IActionResult> AddNewService(InkService service)
        {
            using (_context)
            {
                service.ServiceId = await _context.InkServices.CountAsync() + 1;
                await _context.InkServices.AddAsync(service);
                await _context.SaveChangesAsync();
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }



        private static async Task<string> AddPictureAsync(IFormFile file, string insertPath)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string picturePath = Path.Combine(Directory.GetCurrentDirectory(), insertPath, imageName);
            using (Stream fileStream = new FileStream(picturePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return picturePath.Substring(picturePath.IndexOf("img") - 1).Replace('\\', '/');

        }
    }
}
