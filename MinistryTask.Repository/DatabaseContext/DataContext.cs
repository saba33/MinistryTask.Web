using Microsoft.EntityFrameworkCore;
using MinistryTask.Domain;
using MinistryTask.Domain.Models;

namespace MinistryTask.Repository.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Author>().Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Author>().Property(x => x.PrivateNumber).IsRequired().HasMaxLength(11).IsFixedLength(true);
            builder.Entity<Author>().Property(x => x.Phone).IsRequired().HasMaxLength(50);



            builder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Entity<Product>().Property(x => x.Annotation).IsRequired().HasMaxLength(500);
            builder.Entity<Product>().Property(x => x.ISBN).IsRequired().HasMaxLength(13).IsFixedLength(true);
            builder.Entity<Product>().Property(x => x.ReleaseDate).IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
