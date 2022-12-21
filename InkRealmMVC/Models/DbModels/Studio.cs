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
    public Guid StudioId { get; set; } = Guid.NewGuid();

    [Column("address")]
    [Required]
    public string Address { get; set; } = null!;

    [Column("rental_price_per_month", TypeName = "money")]
    public decimal? RentalPricePerMonth { get; set; }

    [Column("photo_link")]
    public string? PhotoLink { get; set; }
}
