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
    public Guid OrderId { get; set; } = Guid.NewGuid();

    [Column("product_id")]
    public Guid? ProductId { get; set; }

    [Column("create_date", TypeName = "timestamp without time zone")]
    public DateTime? CreateDate { get; set; } = DateTime.Now;

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("finished_date")]
    public DateTime? FinishedDate { get; set; }

    }
