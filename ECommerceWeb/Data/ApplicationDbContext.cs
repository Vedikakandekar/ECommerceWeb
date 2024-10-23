using ECommerceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Role")
                .HasValue<Customer>("Customer")
                .HasValue<Admin>("Admin")
                .HasValue<Seller>("Seller");

            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Clothing-Mens",Description="Explore the Clothing Section curated exclusively for Men.." },
                new Category { Id = 2, Name = "Home Decor",Description="Make your house a home with us.."},
                new Category { Id = 3, Name = "Storage",Description="No Space but a lot of things ? Explore Storage section to make Living spacious.." });

        }

        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Category> Category { get; set; }

    }
}
