using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("clients_needs")]
public partial class ClientsNeed
{
    [Key]
    [Column("clients_needs_id")]
    public Guid ClientsNeedsId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("service_id")]
    public int? ServiceId { get; set; }

    [Column("service_date", TypeName = "timestamp without time zone")]
    public DateTime? ServiceDate { get; set; }

    [Column("order_id")]
    public Guid? OrderId { get; set; }
}
