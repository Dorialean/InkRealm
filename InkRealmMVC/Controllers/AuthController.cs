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
            string[] profs = new string[] { "sketch designer", "tatoo login", "pirsing login" };

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

            int masterId = 0;
            List<InkService> masterChoosedServices = new();
            List<InkSupply> masterChoosedSupplies = new();

            if (master.Photo != null)
            {
                var file = master.Photo;
                master.PhotoLink = await AddPictureAsync(file, MASTER_PICTURE_INFO_PATH);
            }

            using (_context)
            {
                if (await _context.InkMasters.FirstOrDefaultAsync(m => m.Login == master.Login) != null)
                    return await Task.Run(() => BadRequest("Пользователь с таким логином уже существует"));

                master.StudioId = _context.Studios.First(s => s.Address == master.StudioAddress).StudioId;
                master.EncryptedPassword = GeneratePassword(master.Password, master.Registered);

                await _context.InkMasters.AddAsync(new InkMaster()
                {
                    Login = master.Login,
                    Password = master.EncryptedPassword,
                    FirstName = master.FirstName,
                    SecondName = master.SecondName,
                    FatherName = master.FatherName ?? string.Empty,
                    PhotoLink = master.PhotoLink ?? string.Empty,
                    StudioId = master.StudioId,
                    InkPost = master.InkPost,
                    Registered = master.Registered
                });

                await _context.SaveChangesAsync();

                masterId = _context.InkMasters.FirstOrDefault(m => m.Login == master.Login).MasterId;
                masterChoosedServices = await _context.InkServices.Select(s => s).Where(s => master.ServicesTitles.Any(t => t == s.Title)).ToListAsync();
                masterChoosedSupplies = await _context.InkSupplies.Select(s => s).Where(s => master.SuppliesTitles.Any(t => t == s.Title)).ToListAsync();
            }

            if (masterId != 0)
            {
                using (NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO masters_services(master_id, service_id) VALUES ($1, $2)";
                    foreach (int servId in masterChoosedServices.Select(s => s.ServiceId).ToArray())
                    {
                        NpgsqlCommand cmd = new(query, conn)
                        {
                            Parameters =
                            {
                                new(){ Value = masterId},
                                new(){ Value = servId }
                            }
                        };
                        await cmd.ExecuteNonQueryAsync();
                    }
                    //TODO:Добавить возможность юзеру выбрать количество
                    query = "INSERT INTO masters_supplies(master_id, supl_id, amount) VALUES ($1, $2, $3)";
                    foreach (Guid suplId in masterChoosedSupplies.Select(s => s.SuplId).ToArray())
                    {
                        NpgsqlCommand cmd = new(query, conn)
                        {
                            Parameters =
                            {
                                new(){ Value = masterId},
                                new(){ Value = suplId },
                                new(){ Value = 1 }
                            }
                        };
                        await cmd.ExecuteNonQueryAsync();
                    }
                    
                }
            }
            await RegisterNewUser(master);
            return await Task.Run(() =>  RedirectToAction("Index", "MasterArea"));
        }
        
        [HttpGet]
        public async Task<IActionResult> ClientRegister() => await Task.Run(View);
        [HttpPost]
        public async Task<IActionResult> ClientRegister(ClientRegister client)
        {
            if (!IsValidModel(client))
                return await Task.Run(() => BadRequest("Введены не все данные"));

            using (_context)
            {
                if (await _context.InkClients.FirstOrDefaultAsync(c => c.Login == client.Login) != null)
                    return await Task.Run(() => BadRequest("Пользователь с таким логином уже существует!"));
            }

            client.EncryptedPassword = GeneratePassword(client.Password, client.Registered);
            using (NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")))
            {
                string sql = "INSERT INTO ink_clients(first_name,surname,father_name,mobile_phone,email,login,password) VALUES ($1,$2,$3,$4,$5,$6,$7)";
                await conn.OpenAsync();
                await using NpgsqlCommand cmd = new(sql, conn)
                {
                    Parameters =
                    {
                        new(){ Value=client.FirstName},
                        new(){ Value=client.Surname},
                        new(){ Value=client.FatherName?? string.Empty },
                        new(){ Value=client.MobilePhone},
                        new(){ Value=client.Email},
                        new(){ Value=client.Login},
                        new(){ Value=client.EncryptedPassword}
                    }
                };
                await cmd.ExecuteNonQueryAsync();
            }
            await RegisterNewUser(client);
            return await Task.Run(() => RedirectToAction("Index", "ClientArea"));
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
                    var master = _context.InkMasters.First(m => m.Login == loginInfo.Login);
                    if (master.Password == GeneratePassword(loginInfo.Password, master.Registered)) 
                    {
                        RegisterNewUser(loginInfo, Roles.InkWorker);
                        return RedirectToAction("Index", "MasterArea");
                    }
                }
                else if(_context.InkClients.First(c => c.Login == loginInfo.Login) != null)
                {
                    var client = _context.InkClients.First(c => c.Login == loginInfo.Login);
                    if (client.Password == GeneratePassword(loginInfo.Password, client.Registered))
                    {
                        RegisterNewUser(loginInfo, Roles.InkClient);
                        return RedirectToAction("Index", "ClientArea");
                    }
                }
                else
                    return RedirectToAction("ClientRegister", "Auth");
            }
            return BadRequest("Не получилось подключиться к базе данных");
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
                new Claim(ClaimTypes.Role, Roles.InkWorker)
            };
            ClaimsIdentity claimsIdentity = new(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private async Task RegisterNewUser(ClientRegister client)
        {
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, Roles.InkClient)
            };
            ClaimsIdentity claimsIdentity = new(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private async Task RegisterNewUser(LoginModel login, string r)
        {
            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Name, login.Login),
                new Claim(ClaimTypes.Role, r)
            };
            ClaimsIdentity claimsIdentity = new(claims, "Cookies");
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
