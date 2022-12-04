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
    [Required]
    public string Title { get; set; } = null!;

    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    [Column("min_price", TypeName = "money")]
    public decimal MinPrice { get; set; }

    [Column("max_price", TypeName = "money")]
    public decimal? MaxPrice { get; set; }
}
