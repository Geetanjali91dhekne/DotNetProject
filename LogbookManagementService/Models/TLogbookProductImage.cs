using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogbookManagementService.Models;

public partial class TLogbookProductImage
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? ProductImage { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
