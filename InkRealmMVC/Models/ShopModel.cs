using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class ShopModel
    {
        public InkClient? Client { get; set; }
        public List<InkProduct> AllProducts { get; set; }
    }
}
