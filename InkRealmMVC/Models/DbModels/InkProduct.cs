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
    public Guid ProductId { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("each_price", TypeName = "money")]
    public decimal EachPrice { get; set; }

    [Column("props", TypeName = "jsonb")]
    public string? Props { get; set; }

    [Column("photo_link")]
    public string? PhotoLink { get; set; }
}
