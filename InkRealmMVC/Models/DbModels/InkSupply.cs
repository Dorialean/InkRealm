using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("ink_supplies")]
public partial class InkSupply
{
    [Key]
    [Column("supl_id")]
    public Guid SuplId { get; set; }

    [Column("title")]
    [StringLength(40)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }
}
