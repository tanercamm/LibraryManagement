using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; set; }
    }
}