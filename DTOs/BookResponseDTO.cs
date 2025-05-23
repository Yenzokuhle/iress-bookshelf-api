﻿namespace Iress_API_bookshelf.DTOs
{
    public class BookResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }

        public string Genre { get; set; }
    }
}
