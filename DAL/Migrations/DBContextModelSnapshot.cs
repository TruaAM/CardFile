using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Core.Models.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateRegist")
                    .HasColumnType("datetime2(7)");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Core.Models.Material", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Content")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateCreate")
                    .HasColumnType("datetime2(7)");

                b.HasKey("Id");

                b.ToTable("Materials");
            });
        }
    }
}
