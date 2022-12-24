using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;
using System.Diagnostics;
using System.Net;

namespace InkRealmMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly InkRealmContext _context;

        public HomeController(InkRealmContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            InkClient? inkClient = null;
            List<InkService> services = new();
            List<InkProduct> products = new();
            List<InkMaster> masters = new();
            List<Studio> studios = new();

            using (_context)
            {
                if (User.IsInRole(Role.InkClient))
                    inkClient = _context.InkClients.FirstOrDefault(c => c.Login == User.Identity.Name);
                services = _context.InkServices.ToList();
                products = _context.InkProducts.ToList();
                masters = _context.InkMasters.ToList();
                studios = _context.Studios.ToList();
            }

            return View(new HomeModel()
            {
                Client = inkClient,
                InkServices = services,
                InkProducts = products,
                AllMasters = masters,
                AllStudios = studios
            });
        }

        public async Task<IActionResult> Privacy() => await Task.Run(View);

        public async Task<IActionResult> Studio() 
        {
            using (_context)
            {
                List<Studio> allStudios = await _context.Studios.ToListAsync();
                return await Task.Run(() => View(new StudioModel() { AllStudios = allStudios }));
            }
        }

        public async Task<IActionResult> Master()
        {
            List<InkService> allServices = await _context.InkServices.ToListAsync();
            List<MasterToServicesFetchModel> masterToServices = await GetMasterPageInfoList();
            Dictionary<int, List<string>> mastersIdToServiceTitles = new();

            foreach (var master in masterToServices)
            {
                if (mastersIdToServiceTitles.ContainsKey(master.MasterId))
                    mastersIdToServiceTitles[master.MasterId].Add(master.ServiceTitle);
                else
                    mastersIdToServiceTitles.Add(master.MasterId, new() { master.ServiceTitle });
            }
            
            return await Task.Run(() => View(new MasterModel()
            {
                MasterToServiceTitles = mastersIdToServiceTitles,
                AllServices = allServices,
                MasterInfo = masterToServices
            }));
        }

        [Authorize(Roles = Role.InkClient)]
        public async Task<IActionResult> Shop() 
        {
            using (_context)
            {
                List<InkProduct> allProducts = await _context.InkProducts.ToListAsync();
                InkClient? inkClient = null;
                if (User.IsInRole(Role.InkClient))
                {
                    inkClient = await _context.InkClients.FirstOrDefaultAsync(c => c.Login == User.Identity.Name);
                }
                return await Task.Run(() => View(new ShopModel()
                {
                    AllProducts = allProducts,
                    Client = inkClient
                }));
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error() => await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));

        public async Task<IActionResult> Service()
        {
            using (_context)
            {
                List<InkService> services = await _context.InkServices.ToListAsync();
                return await Task.Run(() => View(new ServiceModel()
                {
                    AllServices = services
                }));
            }
            
        }

        private async Task<List<MasterToServicesFetchModel>> GetMasterPageInfoList()
        {
            await using NpgsqlConnection conn = new("Host=localhost;Port=5432;Database=ink_realm;Username=postgres;Password=B&k34RPvvB12F");
            await conn.OpenAsync();

            await using var query = new NpgsqlCommand("SELECT * FROM masters_to_service_info", conn);

            var reader = await query.ExecuteReaderAsync();
            List<MasterToServicesFetchModel> fetch = new();

            while (reader.Read())
            {
                string? fatherName = await reader.IsDBNullAsync(3) ? null : reader.GetString(3);
                string? photoLink = await reader.IsDBNullAsync(6) ? null : reader.GetString(6);
                string? description = await reader.IsDBNullAsync(8) ? null : reader.GetString(8);
                decimal? maxPrice = await reader.IsDBNullAsync(10) ? null : reader.GetDecimal(10);
                int? expirienceYears = await reader.IsDBNullAsync(4) ? null : reader.GetInt32(4);

                fetch.Add(new()
                {
                    MasterId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    SecondName = reader.GetString(2),
                    FatherName = fatherName,
                    ExperienceYears = expirienceYears,
                    Post = reader.GetString(5),
                    PhotoLink = photoLink,
                    ServiceTitle = reader.GetString(7),
                    ServiceDescription = description,
                    ServiceMinPrice = reader.GetDecimal(9),
                    ServiceMaxPrice = maxPrice,
                });
            }

            await conn.CloseAsync();

            return fetch;
        }
    }
}