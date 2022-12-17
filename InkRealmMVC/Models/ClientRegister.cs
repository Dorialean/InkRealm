namespace InkRealmMVC.Models
{
    public class ClientRegister
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? FatherName { get; set; }
        public string? MobilePhone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] EncryptedPassword { get; set; }
        public DateTime Registered { get; set; } = DateTime.Now;
    }
}
