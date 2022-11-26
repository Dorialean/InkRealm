using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("studios")]
public partial class Studio
{
    [Key]
    [Column("studio_id")]
    public Guid StudioId { get; set; }

    [Column("address")]
    public string Address { get; set; } = null!;

    [Column("rental_price_per_month", TypeName = "money")]
    public decimal? RentalPricePerMonth { get; set; }

    [InverseProperty("Studio")]
    public virtual ICollection<InkMaster> InkMasters { get; } = new List<InkMaster>();
}
