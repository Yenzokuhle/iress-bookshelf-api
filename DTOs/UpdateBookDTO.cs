namespace Iress_API_bookshelf.DTOs
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public string ImageName { get; set; }
        public int Year { get; set; }
        public required string Genre { get; set; }
    }
}
