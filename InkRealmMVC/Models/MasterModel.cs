using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class MasterModel
    {
        public List<MasterToServicesFetchModel> MasterInfo { get; set; }
        public Dictionary<int, List<string>> MasterToServiceTitles { get; set; }

        public List<InkService> AllServices { get; set; }
    }
}
