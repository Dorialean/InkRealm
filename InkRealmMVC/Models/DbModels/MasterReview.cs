using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("master_reviews")]
public partial class MasterServices
{
    [Key]
    [Column("master_review_id")]
    public int MasterReviewId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("master_id")]
    [Required]
    public int MasterId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("review")]
    public string? Review { get; set; }
}
