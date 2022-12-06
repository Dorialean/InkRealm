﻿using InkRealmMVC.Models;
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
        [HttpGet]
        public IActionResult Index()
        {
            InkClient? inkClient = null;
            List<InkService> services = new();
            List<InkProduct> products = new();
            List<InkMaster> masters = new();
            //Вот здесь нужно будет сделать вьху под обе штуки
            Dictionary<InkMaster, List<MasterServices>>? masterToReviews = new();
            Dictionary<InkMaster, List<MasterServices>>? masterToServices = new();
            List<Studio> studios = new();

            using (_context)
            {
                if (User.IsInRole(Role.InkClient))
                    inkClient = _context.InkClients.First(c => c.Login == User.Identity.Name);
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
                MasterToReviews = masterToReviews,
                MasterToServices = masterToServices,
                AllStudios = studios
            });
        }

        /*
        public async Task<IActionResult> Index()
        {
           return await Task.Run(View);
        }
        */

        public async Task<IActionResult> Privacy() => await Task.Run(View);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error() => await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
    }
}