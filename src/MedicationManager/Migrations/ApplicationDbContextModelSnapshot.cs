using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MedicationManager.Data;

namespace MedicationManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicationManager.Models.Medication", b =>
                {
                    b.Property<int>("MedID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Dosage");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<int>("RefillRate");

                    b.Property<int?>("SetID");

                    b.Property<int>("TimeOfDay");

                    b.Property<int>("TimesXDay");

                    b.Property<string>("UserId");

                    b.HasKey("MedID");

                    b.HasIndex("SetID");

                    b.HasIndex("UserId");

                    b.ToTable("Medication");
                });

            modelBuilder.Entity("MedicationManager.Models.MedSets", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("SetId");

                    b.Property<int>("ID");

                    b.Property<string>("UserId1");

                    b.HasKey("UserId", "SetId");

                    b.HasIndex("SetId");

                    b.HasIndex("UserId1");

                    b.ToTable("MedSets");
                });

            modelBuilder.Entity("MedicationManager.Models.Set", b =>
                {
                    b.Property<int>("SetID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TimeOfDay");

                    b.HasKey("SetID");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("MedicationManager.Models.User", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Password");

                    b.Property<int>("PermissionLevel");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MedicationManager.Models.Medication", b =>
                {
                    b.HasOne("MedicationManager.Models.Set")
                        .WithMany("MedList")
                        .HasForeignKey("SetID");

                    b.HasOne("MedicationManager.Models.User")
                        .WithMany("AllMeds")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MedicationManager.Models.MedSets", b =>
                {
                    b.HasOne("MedicationManager.Models.Set", "MedSet")
                        .WithMany()
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MedicationManager.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });
        }
    }
}
