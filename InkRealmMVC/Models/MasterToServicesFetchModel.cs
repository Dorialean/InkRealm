namespace InkRealmMVC.Models
{
    public class MasterToServicesFetchModel
    {
        public int MasterId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? FatherName { get; set; }
        public int? ExperienceYears { get; set; }
        public string Post { get; set; }
        public string? PhotoLink { get; set; }
        public string ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public decimal ServiceMinPrice { get; set; }
        public decimal? ServiceMaxPrice { get; set; }
    }
}
