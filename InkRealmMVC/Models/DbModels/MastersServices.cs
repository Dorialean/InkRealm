using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InkRealmMVC.Models.DbModels
{
    [Table("masters_services")]
    [Keyless]
    public class MastersServices
    {
        
        [Column("master_id")]
        [Required]
        public int MasterId { get; set; }

        [Column("service_id")]
        [Required]
        public int ServiceId { get; set; }
    }
}
