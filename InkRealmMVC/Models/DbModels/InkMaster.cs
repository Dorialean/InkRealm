using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("ink_masters")]
public partial class InkMaster
{
    [Key]
    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [Column("second_name")]
    [StringLength(30)]
    public string SecondName { get; set; } = null!;

    [Column("father_name")]
    [StringLength(30)]
    public string? FatherName { get; set; }

    [Column("photo_link")]
    public string? PhotoLink { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("experience_years")]
    public int? ExperienceYears { get; set; }

    [Column("other_info", TypeName = "jsonb")]
    public string? OtherInfo { get; set; }

    [Column("studio_id")]
    public Guid StudioId { get; set; }

    [Column("login")]
    [StringLength(50)]
    public string Login { get; set; } = null!;

    [Column("password")]
    public byte[] Password { get; set; } = null!;

    [Column("registered")]
    public TimeOnly Registered { get; set; }

    [InverseProperty("MasterReviewNavigation")]
    public virtual MasterReview? MasterReviewMasterReviewNavigation { get; set; }

    [InverseProperty("Master")]
    public virtual ICollection<MasterReview> MasterReviewMasters { get; } = new List<MasterReview>();

    [InverseProperty("Master")]
    public virtual MastersSupply? MastersSupply { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("InkMasters")]
    public virtual InkService Service { get; set; } = null!;

    [ForeignKey("StudioId")]
    [InverseProperty("InkMasters")]
    public virtual Studio Studio { get; set; } = null!;
}
