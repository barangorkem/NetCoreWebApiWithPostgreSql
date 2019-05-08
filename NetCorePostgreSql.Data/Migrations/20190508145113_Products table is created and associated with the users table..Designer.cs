﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCorePostgreSql.Data.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NetCorePostgreSql.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190508145113_Products table is created and associated with the users table.")]
    partial class Productstableiscreatedandassociatedwiththeuserstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NetCorePostgreSql.Data.Models.Products", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<float>("Price");

                    b.Property<int>("UserId");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NetCorePostgreSql.Data.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NetCorePostgreSql.Data.Models.Products", b =>
                {
                    b.HasOne("NetCorePostgreSql.Data.Models.Users", "Users")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
