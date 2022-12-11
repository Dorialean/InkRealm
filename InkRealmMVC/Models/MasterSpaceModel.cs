using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class MasterSpaceModel
    {
        public InkMaster Master { get; set; }
        public Studio MasterStudio { get; set; }
        public List<InkService> MasterServices { get; set; }
        public List<InkSupply> MastersSupplies { get; set; }
    }
}
