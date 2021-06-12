﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientPortal.Api;

namespace PatientPortal.Api.Migrations
{
    [DbContext(typeof(PatientPortalDbContext))]
    partial class PatientPortalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PatientPortal.Api.Domain.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMobilePhone")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("PatientPortal.Api.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientPortal.Api.Domain.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("PatientPortal.Api.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Username");

                    b.HasIndex("PatientId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e90416bb-5889-4db7-8e94-273fb63ab8d9"),
                            IsDeleted = false,
                            Password = "10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM=",
                            Username = "bob"
                        },
                        new
                        {
                            Id = new Guid("03797ec9-d933-4049-add4-8ba0803d4371"),
                            IsDeleted = false,
                            Password = "10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE=",
                            Username = "bill"
                        },
                        new
                        {
                            Id = new Guid("6387dac1-a9ea-4d09-bef5-b275b44db78a"),
                            IsDeleted = false,
                            Password = "10000.yIRaKCUp9xVV86/W54uJfQ==.xm3v9vfaAQbYl07xBc3p/EeVnZH1ZFGwRAK0hZ6UTxg=",
                            Username = "patientportal.web"
                        });
                });

            modelBuilder.Entity("PatientPortal.Api.Domain.Patient", b =>
                {
                    b.HasOne("PatientPortal.Api.Domain.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("PatientPortal.Api.Domain.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.Navigation("Contact");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("PatientPortal.Api.Domain.User", b =>
                {
                    b.HasOne("PatientPortal.Api.Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
