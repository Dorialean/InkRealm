﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("master_reviews")]
public partial class MasterReview
{
    [Key]
    [Column("master_review_id")]
    public int MasterReviewId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("review")]
    public string? Review { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("MasterReviews")]
    public virtual InkClient Client { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("MasterReviewMasters")]
    public virtual InkMaster Master { get; set; } = null!;

    [ForeignKey("MasterReviewId")]
    [InverseProperty("MasterReviewMasterReviewNavigation")]
    public virtual InkMaster MasterReviewNavigation { get; set; } = null!;
}