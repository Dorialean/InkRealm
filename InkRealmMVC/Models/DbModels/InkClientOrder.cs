using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InkRealmMVC.Models.DbModels
{
    [Table("ink_client_orders")]
    [PrimaryKey(nameof(ClientId), nameof(OrderId))]
    public class InkClientOrder
    {
        [Required]
        [Column("client_id")]
        public int ClientId { get; set; }
        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; }
    }
}
