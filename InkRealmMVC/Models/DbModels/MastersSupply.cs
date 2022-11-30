﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

[Table("masters_supplies")]
public partial class MastersSupply
{
    [Key]
    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("supl_id")]
    public Guid? SuplId { get; set; }

    [Column("amount")]
    public int? Amount { get; set; }
}
