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
    public TimeOnly Registered { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    [Column("ink_post")]
    public string InkPost { get; set; } = null!;
}