using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogbookManagementService.Models
{
    [Table("PK_BDPlanning")]
    public class PKBDPlanning
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
