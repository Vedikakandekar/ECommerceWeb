using ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<ShippingAddress> ShippingAddress { get; set; }

        public DbSet<OrderItemStatus> OrderItemStatus { get; set; }

        public DbSet<Likes> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Role")
                .HasValue<Customer>("Customer")
                .HasValue<Admin>("Admin")
                .HasValue<Seller>("Seller");

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Likes>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.customer)
                .WithMany(u => u.Likes) // Assuming the User class has a Likes collection
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.product)
                .WithMany(p => p.Likes) // Assuming the Product class has a Likes collection
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Clothing-Mens", Description = "Explore the Clothing Section curated exclusively for Men.." },
                    new Category { Id = 2, Name = "Home Decor", Description = "Make your house a home with us.." },
                    new Category { Id = 3, Name = "Storage", Description = "No Space but a lot of things ? Explore Storage section to make Living spacious.." },
                new Category { Id = 4, Name = "Electronics", Description = "All Your Electric stuff goes here...check out latest innovations .." });
            modelBuilder.Entity<Products>().HasData(

            new Products { Id = 1, Name = "TV", Description = "60Inch", Price = 50000, stock = 10, CategoryId = 4, ImageUrl = "", SellerId = "f300ff54-1b2e-43f9-a700-b04ff20d6153" },
            new Products { Id = 2, Name = "Laptop", Description = "i10", Price = 50000, stock = 20, CategoryId = 4, ImageUrl = "", SellerId = "f300ff54-1b2e-43f9-a700-b04ff20d6153" },
            new Products { Id = 3, Name = "TShirt", Description = "Black Tshirt", Price = 1000, stock = 100, CategoryId = 1, ImageUrl = "", SellerId = "f300ff54-1b2e-43f9-a700-b04ff20d6153" });
        }
    }
}
