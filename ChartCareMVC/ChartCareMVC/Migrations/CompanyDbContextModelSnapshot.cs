﻿// <auto-generated />
using System;
using ChartCareData.Data;
using ChartCareData.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChartCareMVC.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    partial class CompanyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChartCareMVC.Areas.Identity.Data.CompanyUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ChartCareMVC.Models.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlanID");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("ChartCareMVC.Models.Features", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AbbreviatedDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Features", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AbbreviatedDescription = "Create up to 50 employee accounts",
                            Description = "Create up to 50 employee accounts for your organization",
                            Name = "Employee Count Free"
                        },
                        new
                        {
                            ID = 2,
                            AbbreviatedDescription = "Create up to 500 employee accounts",
                            Description = "Create up to 500 employee accounts for your organization",
                            Name = "Employee Count Standard"
                        },
                        new
                        {
                            ID = 3,
                            AbbreviatedDescription = "No limit on employee accounts created",
                            Description = "No limit on employee accounts created for your organization",
                            Name = "Employee Count Premium"
                        },
                        new
                        {
                            ID = 4,
                            Description = "Instantly updates patient charts with live data, ensuring that any modifications or new entries are reflected across all connected devices without delay. ",
                            Name = "Real Time Charting"
                        },
                        new
                        {
                            ID = 5,
                            Description = "Ability to create a personalized list of patients for each company user",
                            Name = "Patient Workload Customization"
                        },
                        new
                        {
                            ID = 6,
                            Description = "Provides essential insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include: Patient Visit Frequency, Patient Demographics, Treatment Success Rates, Average Length of Stay, Medication Adherence,",
                            Name = "Basic Analytics"
                        },
                        new
                        {
                            ID = 7,
                            Description = "Provides additional insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include: Cost-Per-Treatment Analysis, Longitudinal Patient Progress Tracking, Readmission Rates, Patient Satisfaction Scores",
                            Name = "Advanced Analytics"
                        },
                        new
                        {
                            ID = 8,
                            Description = "Automatically sends alerts and reminders to users about important events, such as upcoming appointments, medication schedules, and critical patient status changes.",
                            Name = "Automated Notifications"
                        },
                        new
                        {
                            ID = 9,
                            Description = "Gives access to priority customer support with faster response times, ensuring any issues or inquiries are addressed promptly.",
                            Name = "Priority Support"
                        });
                });

            modelBuilder.Entity("ChartCareMVC.Models.PlanFeatures", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.HasKey("PlanId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("PlanFeatures", (string)null);

                    b.HasData(
                        new
                        {
                            PlanId = 1,
                            FeatureId = 1
                        },
                        new
                        {
                            PlanId = 1,
                            FeatureId = 4
                        },
                        new
                        {
                            PlanId = 1,
                            FeatureId = 5
                        },
                        new
                        {
                            PlanId = 2,
                            FeatureId = 2
                        },
                        new
                        {
                            PlanId = 2,
                            FeatureId = 6
                        },
                        new
                        {
                            PlanId = 2,
                            FeatureId = 8
                        },
                        new
                        {
                            PlanId = 3,
                            FeatureId = 3
                        },
                        new
                        {
                            PlanId = 3,
                            FeatureId = 7
                        },
                        new
                        {
                            PlanId = 3,
                            FeatureId = 9
                        });
                });

            modelBuilder.Entity("ChartCareMVC.Models.PricingPlan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("PlanName")
                        .HasColumnType("int");

                    b.Property<string>("PlanNameString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PlanPrice")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Pricing_Plan", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            PlanName = 0,
                            PlanNameString = "Free",
                            PlanPrice = 0f
                        },
                        new
                        {
                            ID = 2,
                            PlanName = 1,
                            PlanNameString = "Standard",
                            PlanPrice = 19.99f
                        },
                        new
                        {
                            ID = 3,
                            PlanName = 2,
                            PlanNameString = "Premium",
                            PlanPrice = 29.99f
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ChartCareMVC.Areas.Identity.Data.CompanyUser", b =>
                {
                    b.HasOne("ChartCareMVC.Models.Company", "Company")
                        .WithMany("CompanyUsers")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ChartCareMVC.Models.Company", b =>
                {
                    b.HasOne("ChartCareMVC.Models.PricingPlan", "PricingPlan")
                        .WithMany("Companies")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PricingPlan");
                });

            modelBuilder.Entity("ChartCareMVC.Models.PlanFeatures", b =>
                {
                    b.HasOne("ChartCareMVC.Models.Features", "Feature")
                        .WithMany("PlanFeatureLinks")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChartCareMVC.Models.PricingPlan", "PricingPlan")
                        .WithMany("PlanFeatureLinks")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("PricingPlan");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ChartCareMVC.Areas.Identity.Data.CompanyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ChartCareMVC.Areas.Identity.Data.CompanyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChartCareMVC.Areas.Identity.Data.CompanyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ChartCareMVC.Areas.Identity.Data.CompanyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChartCareMVC.Models.Company", b =>
                {
                    b.Navigation("CompanyUsers");
                });

            modelBuilder.Entity("ChartCareMVC.Models.Features", b =>
                {
                    b.Navigation("PlanFeatureLinks");
                });

            modelBuilder.Entity("ChartCareMVC.Models.PricingPlan", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("PlanFeatureLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
