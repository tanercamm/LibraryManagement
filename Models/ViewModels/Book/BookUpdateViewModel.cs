using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.ViewModels.Book
{
    public class BookUpdateViewModel : BookCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
