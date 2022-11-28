using InkRealmMVC.Models.DbModels;

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
        public TimeOnly Registered { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        public List<string> ServicesTitles { get; set; } = null!;
        public string StudioAddress { get; set; }
        public int StudioId { get; set; }
        public string InkPost { get; set; }
        public List<Studio>? AllStudios { get; set; }
        public List<InkService>? AllServices { get; set; }

        public List<string>? AllProfs { get; set; }

    }
}
