﻿// <auto-generated />
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerceWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241028101039_AddedProductTable")]
    partial class AddedProductTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Explore the Clothing Section curated exclusively for Men..",
                            Name = "Clothing-Mens"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Make your house a home with us..",
                            Name = "Home Decor"
                        },
                        new
                        {
                            Id = 3,
                            Description = "No Space but a lot of things ? Explore Storage section to make Living spacious..",
                            Name = "Storage"
                        });
                });

            modelBuilder.Entity("ECommerce.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "60Inch",
                            Name = "TV",
                            Price = 50000.0,
                            stock = 10
                        },
                        new
                        {
                            Id = 2,
                            Description = "i10",
                            Name = "Laptop",
                            Price = 50000.0,
                            stock = 20
                        },
                        new
                        {
                            Id = 3,
                            Description = "Black Tshirt",
                            Name = "TShirt",
                            Price = 1000.0,
                            stock = 100
                        });
                });

            modelBuilder.Entity("ECommerce.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Role").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ECommerce.Models.Admin", b =>
                {
                    b.HasBaseType("ECommerce.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ECommerce.Models.Customer", b =>
                {
                    b.HasBaseType("ECommerce.Models.User");

                    b.Property<string>("Notifications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("ECommerce.Models.Seller", b =>
                {
                    b.HasBaseType("ECommerce.Models.User");

                    b.HasDiscriminator().HasValue("Seller");
                });
#pragma warning restore 612, 618
        }
    }
}
