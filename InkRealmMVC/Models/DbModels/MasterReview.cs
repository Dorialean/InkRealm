using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("master_reviews")]
[PrimaryKey(nameof(ClientId),nameof(MasterId))]
public partial class MasterReviews
{
    [Column("client_id")]
    [Required]
    public int ClientId { get; set; }

    [Column("master_id")]
    [Required]
    public int MasterId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("review")]
    public string? Review { get; set; }
}
