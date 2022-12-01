using InkRealmMVC.Models;
using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using NpgsqlTypes;
using NuGet.Protocol.Plugins;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InkRealmMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly InkRealmContext _context;
        private readonly SHA256 sha256 = SHA256.Create();

        const string MASTER_PICTURE_INFO_PATH = "wwwroot/img/masters_img/info";
        const string MASTER_PICTURE_WORK_PATH = "wwwroot/img/masters_img/works";
        const string CLIENT_PICTURE_PATH = "wwwroot/img/clients_img";

        public AuthController(InkRealmContext context)
        {
            _context = context;
        }

        //TODO:Здесь хардкод, надо будет получать из ENUM в БД
        [HttpGet]
        public async Task<IActionResult> MasterRegister()
        {
            List<Studio> allStudios;
            List<InkService> allInkServices;
            List<InkSupply> allInkSupplies;
            string[] profs = new string[] { "sketch designer", "tatoo master", "pirsing master" };

            using (_context)
            {
                allStudios = await _context.Studios.ToListAsync();
                allInkServices = await _context.InkServices.ToListAsync();
                allInkSupplies = await _context.InkSupplies.ToListAsync();
            }

            return await Task.Run(() => View(new MasterRegister()
            {
                AllServices = allInkServices,
                AllStudios = allStudios,
                AllProfs = profs.ToList(),
                AllSupplies = allInkSupplies
            }));
        }
        [HttpPost]
        public async Task<IActionResult> MasterRegister(MasterRegister master)
        {
            if (!IsValidModel(master))
                return await Task.Run(() => BadRequest("Введены не все данные"));

            if (master.Photo != null)
            {
                var file = master.Photo;
                master.PhotoLink = await AddPictureAsync(file, MASTER_PICTURE_INFO_PATH);
            }

            using (_context)
            {
                if (await _context.InkMasters.FirstOrDefaultAsync(m => m.Login == master.Login) != null)
                    return await Task.Run(() => BadRequest("Пользователь с таким логином уже существует"));

                master.EncryptedPassword = GeneratePassword(master.Password, master.Registered);
                master.StudioId = _context.Studios.First(s => s.Address == master.StudioAddress).StudioId;

                string sql = $"""
                    INSERT INTO ink_masters(first_name, 
                        second_name, 
                        father_name, 
                        photo_link, 
                        experience_years,  
                        studio_id, 
                        login, 
                        password, 
                        ink_post,
                        registered) VALUES ('{master.FirstName}',
                        '{master.SecondName}',
                        '{master.FatherName}',
                        '{master.PhotoLink}',
                        {master.ExperienceYears},
                        '{master.StudioId}',
                        '{master.Login}',
                        '{Encoding.UTF8.GetString(master.EncryptedPassword)}',
                        '{master.InkPost}',
                        '{master.Registered}');
                    """;
                using (NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")))
                {
                    conn.Open();
                    NpgsqlCommand cmd = new(sql, conn);
                    cmd.ExecuteNonQuery();

                    int masterId = 0;
                    sql = $""" SELECT master_id FROM ink_masters WHERE login = '{master.Login}' """;
                    cmd = new(sql, conn);
                    var res = await cmd.ExecuteScalarAsync();

                    if (res != null)
                    {
                        int.TryParse(res.ToString(),out masterId);
                    }

                    if (masterId != 0)
                    {
                        foreach (string serviceTitle in master.ServicesTitles)
                        {
                            sql = $"""INSERT INTO masters_services(master_id, service_id) VALUES ({masterId}, {_context.InkServices.First(s => s.Title == serviceTitle).ServiceId});""";
                            cmd = new(sql, conn);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        foreach (string supplyTitle in master.SuppliesTitles)
                        {
                            sql = $"""INSERT INTO masters_supplies(master_id, supl_id, amount) VALUES ({masterId}, {_context.InkSupplies.First(s => s.Title == supplyTitle).SuplId, 1});""";
                            cmd = new(sql, conn);
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            await RegisterNewUser(master);
            return await Task.Run(() =>  RedirectToAction("MasterArea", "MasterAreaController"));
        }
        
        [HttpGet]
        public async Task<IActionResult> ClientRegister() => await Task.Run(View);
        [HttpPost]
        public IActionResult ClientRegister(ClientRegister client)
        {
            if (!IsValidModel(client))
                return BadRequest("Введены не все данные");

            using (_context)
            {
                if (_context.InkClients.FirstOrDefault(c => c.Login == client.Login) != null)
                    return BadRequest(() => BadRequest("Пользователь с таким логином уже существует!"));

                client.EncryptedPassword = GeneratePassword(client.Password, client.Registered);

                string sql = $"""
                    INSERT INTO ink_clients(first_name,
                                            surname,
                                            father_name,
                                            mobile_phone,
                                            email,
                                            login,
                                            password) VALUES ('{client.FirstName}',
                                                            '{client.Surname}',
                                                            '{client.FatherName}',
                                                            '{client.MobilePhone}',
                                                            '{client.Email}',
                                                            '{client.Login}',
                                                            '{Encoding.UTF8.GetString(client.EncryptedPassword)}'); 
                    """;

                using (NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")))
                {
                    conn.Open();
                    NpgsqlCommand cmd = new(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ClientArea", "ClientAreaController");
        }

        [HttpGet]
        public async Task<IActionResult> Login()  => await Task.Run(View);

        [HttpPost]
        public IActionResult Login(LoginModel loginInfo)
        {
            using (_context)
            {
                if (_context.InkMasters.First(m => m.Login == loginInfo.Login) != null)
                {
                    //Method
                }
                else if(_context.InkClients.First(c => c.Login == loginInfo.Login) != null)
                {
                    //Method
                }
                else
                {
                    return RedirectToAction("ClientRegister", "Auth");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await ControllerContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }



        private static bool IsValidModel(MasterRegister master)
        {
            if (string.IsNullOrEmpty(master.Login))
                return false;
            if (string.IsNullOrEmpty(master.Password))
                return false;
            if (string.IsNullOrEmpty(master.FirstName))
                return false;
            if (string.IsNullOrEmpty(master.SecondName))
                return false;
            if (master.ServicesTitles.Count == 0)
                return false;
            if (string.IsNullOrEmpty(master.StudioAddress))
                return false;
            return true;
        }
        private static bool IsValidModel(ClientRegister client)
        {
            if (string.IsNullOrEmpty(client.Login))
                return false;
            if (string.IsNullOrEmpty(client.Password))
                return false;
            if (string.IsNullOrEmpty(client.FirstName))
                return false;
            if (string.IsNullOrEmpty(client.Surname))
                return false;
            return true;
        }

        private async Task<string> AddPictureAsync(IFormFile file, string insertPath)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string picturePath = Path.Combine(Directory.GetCurrentDirectory(), insertPath, imageName);
            using (Stream fileStream = new FileStream(picturePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return picturePath;

        }

        private async Task RegisterNewUser(MasterRegister master)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, master.Login),
                new Claim(ClaimTypes.Role, master.InkPost)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private async Task RegisterNewUser(ClientRegister master)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, master.Login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }


        private byte[] GeneratePassword(string password, TimeOnly date)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(date.ToString());
            byte[] resToEncrypt = passBytes.Concat(saltBytes).ToArray();
            return sha256.ComputeHash(resToEncrypt);
        }
        private byte[] GeneratePassword(string password, DateTime date)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(date.ToString());
            byte[] resToEncrypt = passBytes.Concat(saltBytes).ToArray();
            return sha256.ComputeHash(resToEncrypt);
        }

    }
}
