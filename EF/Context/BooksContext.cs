using Iress_API_bookshelf.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Iress_API_bookshelf.EF.Context
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            /**
             * Validation on Database level(Option 1)
             */
            //modelBuilder.Entity<Book>().Property(property => property.Id).IsRequired();
            //modelBuilder.Entity<Book>().Property(property => property.Title).IsRequired();
            //modelBuilder.Entity<Book>().Property(property => property.Description).IsRequired();
            //modelBuilder.Entity<Book>().Property(property => property.Author).IsRequired();
        }

    }
}
