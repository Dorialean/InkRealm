using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InkRealmMVC.Models.DbModels
{
    [Table("masters_services")]
    public class MastersServices
    {
        [Key]
        [Column("master_id")]
        [Required]
        public int MasterId { get; set; }

        [Column("service_id")]
        [Required]
        public int ServiceId { get; set; }
    }
}
