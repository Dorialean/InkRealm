using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("ink_services")]
public partial class InkService
{
    [Key]
    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("title")]
    [StringLength(80)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("min_price", TypeName = "money")]
    public decimal MinPrice { get; set; }

    [Column("max_price", TypeName = "money")]
    public decimal? MaxPrice { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<ClientsNeed> ClientsNeeds { get; } = new List<ClientsNeed>();

    [InverseProperty("Service")]
    public virtual ICollection<InkMaster> InkMasters { get; } = new List<InkMaster>();
}
