// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VisitorCounterAPI.Data;

namespace VisitorCounterAPI.Migrations
{
    [DbContext(typeof(VisitorCounterAPIContext))]
    partial class VisitorCounterAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("VisitorCounterAPI.Models.Visitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Visits")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Visitor");
                });
#pragma warning restore 612, 618
        }
    }
}
