using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class ClientSpaceModel
    {
        public InkClient Client { get; set; }
        public List<Order>? Orders { get; set; }
        public List<ClientsNeed>? ClientNeeds { get; set; }
        public List<InkService>? ClientServices { get; set; }
        public List<InkMaster>? MastersToCome { get; set; }
        public List<MasterReviews>? ClientReviewd { get; set; }
        public List<InkProduct>? WantedProducts { get; set; }
        public List<InkProduct>? OrderedProducts { get; set; }

    }
}
