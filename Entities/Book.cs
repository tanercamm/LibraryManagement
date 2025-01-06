using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Publisher { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Sayfa sayısı 0 ile 5000 aralığında olmalıdır!")]
        public int PageCount { get; set; }

        public string? ImageUrl { get; set; }
    }
}
