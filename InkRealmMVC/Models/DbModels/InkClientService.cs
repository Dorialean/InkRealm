using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InkRealmMVC.Models.DbModels
{
    [Table("ink_client_services")]
    [PrimaryKey(nameof(ClientId),nameof(MasterId),nameof(ServiceId))]
    public class InkClientService
    {
        [Required]
        [Column("client_id")]
        public int ClientId { get; set; }
        [Required]
        [Column("master_id")]
        public int MasterId { get; set; }
        [Required]
        [Column("service_id")]
        public int ServiceId { get; set; }
        [Required]
        [Column("service_date", TypeName = "timestamp without time zone")]
        public DateTime ServiceDate { get; set; } = DateTime.Now;
        [Column("service_finished", TypeName = "timestamp without time zone")]
        public DateTime? ServiceFinished { get; set; }
        [Column("progress")]
        public string? Progress { get; set; }
    }
}
