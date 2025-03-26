﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _CampusFinderInfrastructure.Data.AppDbContext;

#nullable disable

namespace _CampusFinderInfrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College", b =>
                {
                    b.Property<int>("CollegeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollegeID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("StandardFees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UniversityID")
                        .HasColumnType("int");

                    b.HasKey("CollegeID");

                    b.HasIndex("UniversityID");

                    b.ToTable("Colleges", (string)null);
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College_Diploma", b =>
                {
                    b.Property<int>("CollegeID")
                        .HasColumnType("int");

                    b.Property<int>("DiplomaID")
                        .HasColumnType("int");

                    b.Property<string>("Min_Grade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirments")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CollegeID", "DiplomaID");

                    b.HasIndex("CollegeID");

                    b.HasIndex("DiplomaID");

                    b.ToTable("CollegeDiplomas");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College_English", b =>
                {
                    b.Property<int>("CollegeID")
                        .HasColumnType("int");

                    b.Property<int>("TestID")
                        .HasColumnType("int");

                    b.Property<decimal>("Min_Score")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CollegeID", "TestID");

                    b.HasIndex("CollegeID");

                    b.HasIndex("TestID");

                    b.ToTable("CollegeEnglishTests");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.Diploma", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Diplomas");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.English_Test", b =>
                {
                    b.Property<int>("TestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TestID");

                    b.ToTable("EnglishTests");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.Major", b =>
                {
                    b.Property<int>("MajorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorID"));

                    b.Property<int>("CollegeID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MajorID");

                    b.HasIndex("CollegeID");

                    b.ToTable("Majors", (string)null);
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.University", b =>
                {
                    b.Property<int>("UniversityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UniversityID"));

                    b.Property<string>("DegreeType")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Degree Type");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Description");

                    b.Property<string>("LearningStyle")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "LearningStyle");

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasAnnotation("Relational:JsonPropertyName", "Location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PictureURL")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Picture URL");

                    b.Property<string>("PrimaryLanguage")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "PrimaryLanguage");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "Rank");

                    b.Property<string>("RequiredDocuments")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Required Documents");

                    b.Property<string>("UniEmail")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "UniEmail");

                    b.Property<string>("UniPhone")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "UniPhone");

                    b.Property<string>("UniversityType")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "UniversityType");

                    b.Property<string>("WebsiteURL")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Website URL");

                    b.HasKey("UniversityID");

                    b.HasIndex("Location");

                    b.HasIndex("Name");

                    b.HasIndex("Rank");

                    b.ToTable("Universities", (string)null);
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College", b =>
                {
                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.University", "University")
                        .WithMany("Colleges")
                        .HasForeignKey("UniversityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College_Diploma", b =>
                {
                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.College", "College")
                        .WithMany("CollegeDiplomas")
                        .HasForeignKey("CollegeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.Diploma", "Diploma")
                        .WithMany("CollegeDiplomas")
                        .HasForeignKey("DiplomaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");

                    b.Navigation("Diploma");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College_English", b =>
                {
                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.College", "College")
                        .WithMany("CollegeEnglishTests")
                        .HasForeignKey("CollegeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.English_Test", "English_Test")
                        .WithMany("CollegeEnglishTests")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");

                    b.Navigation("English_Test");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.Major", b =>
                {
                    b.HasOne("_CampusFinderCore.Entities.UniversityEntities.College", "College")
                        .WithMany("Majors")
                        .HasForeignKey("CollegeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.College", b =>
                {
                    b.Navigation("CollegeDiplomas");

                    b.Navigation("CollegeEnglishTests");

                    b.Navigation("Majors");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.Diploma", b =>
                {
                    b.Navigation("CollegeDiplomas");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.English_Test", b =>
                {
                    b.Navigation("CollegeEnglishTests");
                });

            modelBuilder.Entity("_CampusFinderCore.Entities.UniversityEntities.University", b =>
                {
                    b.Navigation("Colleges");
                });
#pragma warning restore 612, 618
        }
    }
}
