﻿using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Models
{
    public class HomeModel
    {
        public InkClient? Client { get; set; }
        public List<InkService>? InkServices { get; set; }
        public List<InkProduct>? InkProducts { get; set; }

        public List<InkMaster>? AllMasters { get; set; }
        public Dictionary<InkMaster, List<MasterReviews>>? MasterToReviews { get; set; }
        public Dictionary<InkMaster, List<MasterReviews>>? MasterToServices { get; set; }
        public List<Studio> AllStudios { get; set; }
    }
}