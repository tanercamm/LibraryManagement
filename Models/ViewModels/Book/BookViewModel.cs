namespace LibraryManagement.Models.ViewModels.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Publisher { get; set; }

        public int PageCount { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
