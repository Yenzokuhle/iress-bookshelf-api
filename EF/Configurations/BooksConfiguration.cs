using Iress_API_bookshelf.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iress_API_bookshelf.EF.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        /**
            * Validation on Database level(Option 2)
            */
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(property => property.Title).IsRequired();
            builder.Property(property => property.Description).IsRequired();
            builder.Property(property => property.Genre).IsRequired();
            builder.Property(property => property.Author).IsRequired();
            builder.Property(property => property.Year).IsRequired();
        }

    }
}
