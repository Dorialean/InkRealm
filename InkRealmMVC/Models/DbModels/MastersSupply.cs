using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("masters_supplies")]
[PrimaryKey(nameof(MasterId),nameof(SuplId))]
public partial class MastersSupply
{
    [Column("master_id")]
    [Required]
    public int MasterId { get; set; }

    [Column("supl_id")]
    [Required]
    public Guid SuplId { get; set; }

    [Column("amount")]
    public int? Amount { get; set; }
}
