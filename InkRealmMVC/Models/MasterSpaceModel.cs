using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class MasterSpaceModel
    {
        public InkMaster Master { get; set; }
        public Studio MasterStudio { get; set; }
        public List<MastersServices> MasterServices { get; set; }
        public List<MastersSupply> MastersSupplies { get; set; }
        public List<ClientsNeed> MastersWorks { get; set; }
    }
}
