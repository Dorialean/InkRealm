using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("ink_clients")]
public partial class InkClient
{
    [Key]
    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [Column("surname")]
    [StringLength(30)]
    public string Surname { get; set; } = null!;

    [Column("father_name")]
    [StringLength(30)]
    public string? FatherName { get; set; }

    [Column("mobile_phone")]
    [StringLength(20)]
    public string? MobilePhone { get; set; }

    [Column("email")]
    [StringLength(60)]
    public string? Email { get; set; }

    [Column("client_needs_id")]
    public Guid? ClientNeedsId { get; set; }

    [Column("login")]
    [StringLength(50)]
    public string Login { get; set; } = null!;

    [Column("password")]
    public byte[] Password { get; set; } = null!;

    [Column("registered", TypeName = "timestamp without time zone")]
    public DateTime Registered { get; set; }
}
