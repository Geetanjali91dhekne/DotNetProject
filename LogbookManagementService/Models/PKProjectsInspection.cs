using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogbookManagementService.Models
{
    [Table("PK_SpecialProjectsInspection")]
    public class PKProjectsInspection
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("Status")]
        public string? Status { get; set; }
    }
}
