namespace LibraryManagement.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Publisher { get; set; }

        public int PageCount { get; set; }

        public string? ImageUrl { get; set; }
    }
}
