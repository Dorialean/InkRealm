using InkRealmMVC.Models.DbModels;
using System.Reflection.Metadata.Ecma335;

namespace InkRealmMVC.Models
{
    public class MasterRegister
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] EncryptedPassword { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string? FatherName { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoLink { get; set; }
        public int? ExperienceYears { get; set; }
        public string? OtherInfo { get; set; }
        public DateTime Registered { get; set; } = DateTime.Now;
        public List<string> ServicesTitles { get; set; } = null!;
        public List<string>? SuppliesTitles { get; set; }
        public string StudioAddress { get; set; }
        public Guid StudioId { get; set; }
        public string InkPost { get; set; }
        public List<Studio>? AllStudios { get; set; }
        public List<InkService>? AllServices { get; set; }

        public List<string>? AllProfs { get; set; }
        public List<InkSupply>? AllSupplies { get; set; }

    }
}
