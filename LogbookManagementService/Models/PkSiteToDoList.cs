using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogbookManagementService.Models;

[Table("PK_SiteToDoList")]
public partial class PkSiteToDoList
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Title")]
    public string? Title { get; set; }
    [Column("Status")]
    public string? Status { get; set; }
}
