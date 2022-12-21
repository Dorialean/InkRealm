using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class ClientSpaceModel
    {
        public InkClient Client { get; set; }
        public List<InkService>? NeededServices { get; set; }
        public List<InkMaster>? MastersToCome { get; set; }
        public List<MasterReviews>? ClientReviewd { get; set; }
        public List<InkClientService>? ClientServices { get; set; }
        public List<InkProduct>? OrderedProducts { get; set; }

    }
}
