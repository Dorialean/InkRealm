using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class AppointmentClient
    {
        public InkClient Client { get; set; }
        public List<InkMaster>? AllMasters { get; set; }
        public int? ChoosedMasterId { get; set; }
        public List<InkService>? AllServices { get; set; }
        public int? ChoosedServiceId { get; set; }
        public DateTime? ServiceDate { get; set; } = DateTime.Now.AddDays(7);
        public string? Progress { get; set; } = "waiting";
    }
}
