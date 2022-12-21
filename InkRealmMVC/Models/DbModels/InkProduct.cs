using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("ink_products")]
public partial class InkProduct
{
    [Key]
    [Column("product_id")]
    public Guid ProductId { get; set; } = Guid.NewGuid();

    [Column("title")]
    [StringLength(50)]
    [Required]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("each_price", TypeName = "money")]
    public decimal EachPrice { get; set; }

    [Column("photo_link")]
    public string? PhotoLink { get; set; }
}
