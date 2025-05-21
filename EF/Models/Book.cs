namespace Iress_API_bookshelf.EF.Models
{
    public class Book
    {
        /**
        * Validation on Local level
        */
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public string ImageName { get; set; }
        public required int Year { get; set; }
        public required string Genre { get; set; }
    }
}
