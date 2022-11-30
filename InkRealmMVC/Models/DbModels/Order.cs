using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("orders")]
public partial class Order
{
    [Key]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("product_id")]
    public Guid? ProductId { get; set; }

    [Column("create_date", TypeName = "timestamp without time zone")]
    public DateTime? CreateDate { get; set; }
}
