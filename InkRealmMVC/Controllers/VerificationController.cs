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
    public class VerificationController : Controller
    {
        private readonly InkRealmContext _context;
        private readonly SHA256 _sha256 = SHA256.Create();

        const string MASTER_PICTURE_INFO_PATH = "wwwroot/img/masters_img/info";
        const string CLIENT_PICTURE_PATH = "wwwroot/img/clients_img";

        public VerificationController(InkRealmContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> MasterRegister()
        {
            List<Studio> allStudios;
            List<InkService> allInkServices;
            List<InkSupply> allInkSupplies;
            string[] profs = { "sketch designer", "tatoo login", "pirsing login" };

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
            return await Task.Run(() =>  RedirectToAction("Index", "Master"));
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
                        new(){ Value=client.FatherName ?? string.Empty },
                        new(){ Value=client.MobilePhone ?? string.Empty},
                        new(){ Value=client.Email ?? string.Empty},
                        new(){ Value=client.Login},
                        new(){ Value=client.EncryptedPassword}
                    }
                };
                await cmd.ExecuteNonQueryAsync();
            }
            await RegisterNewUser(client);
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }

        [HttpGet]
        public IActionResult Auth() 
        {
            if (User.IsInRole(Role.InkWorker))
                return RedirectToAction("Index", "Master");
            else if(User.IsInRole(Role.InkClient))
                return RedirectToAction("Index", "Client");
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginModel loginInfo)
        {
            using (_context) 
            {
                if (await _context.InkMasters.FirstOrDefaultAsync(m => m.Login == loginInfo.Login) != null)
                {
                    var master = await _context.InkMasters.FirstAsync(m => m.Login == loginInfo.Login);
                    byte[] genPass = GeneratePassword(loginInfo.Password, master.Registered);
                    if (master.Password.SequenceEqual(genPass))
                    {
                        await RegisterNewUser(loginInfo, Role.InkWorker);
                        return await Task.Run(() => RedirectToAction("Index", "Home"));
                    }
                    else
                        return await Task.Run(() => BadRequest("Нерпавильный пароль"));
                }
                else if (await _context.InkClients.FirstOrDefaultAsync(c => c.Login == loginInfo.Login) != null)
                {
                    var client = await _context.InkClients.FirstAsync(c => c.Login == loginInfo.Login);
                    if (client.Password.SequenceEqual(GeneratePassword(loginInfo.Password, client.Registered)))
                    {
                        await RegisterNewUser(loginInfo, Role.InkClient);
                        return await Task.Run(() => RedirectToAction("Index", "Home"));
                    }
                    else
                        return await Task.Run(() => BadRequest("Неправильный пароль"));
                }
                else
                    return await Task.Run(() => RedirectToAction("ClientRegister"));
            }
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

        private async Task RegisterNewUser(MasterRegister master)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, master.Login),
                new Claim(ClaimTypes.Role, Role.InkWorker)
            };
            ClaimsIdentity claimsIdentity = new(claims, "Cookies");
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private async Task RegisterNewUser(ClientRegister client)
        {
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, Role.InkClient)
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


        private byte[] GeneratePassword(string password, TimeOnly time)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(time.ToString());
            byte[] resToEncrypt = passBytes.Concat(saltBytes).ToArray();
            return _sha256.ComputeHash(resToEncrypt);
        }

        private byte[] GeneratePassword(string password, string time)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(time);
            byte[] resToEncrypt = passBytes.Concat(saltBytes).ToArray();
            return _sha256.ComputeHash(resToEncrypt);
        }

        private byte[] GeneratePassword(string password, DateTime date)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(date.ToString());
            byte[] resToEncrypt = passBytes.Concat(saltBytes).ToArray();
            return _sha256.ComputeHash(resToEncrypt);
        }

    }
}
