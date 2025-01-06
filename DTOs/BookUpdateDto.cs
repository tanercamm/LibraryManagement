using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs
{
    public class BookUpdateDto : BookCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
