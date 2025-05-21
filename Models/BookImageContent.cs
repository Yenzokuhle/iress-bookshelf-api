namespace Iress_API_bookshelf.Models
{
    public class BookImageContent
    {
        public string BookId { get; set; }
        public IFormFile? BookImage { get; set; }
    }
}
