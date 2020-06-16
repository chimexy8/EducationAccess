﻿// <auto-generated />
using System;
using FlashMoneyApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlashMoneyApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200222164017_cardaddprocess")]
    partial class cardaddprocess
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlashMoneyApi.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.ActivityModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityModel");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.AirtimeRecharge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("DestinationPhone");

                    b.Property<string>("RequestId");

                    b.Property<string>("TransactionId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AirtimeRecharge");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.CardAddProcess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsProcessed");

                    b.Property<string>("Reference");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("CardAddProcess");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.CardDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Amount");

                    b.Property<string>("CVV");

                    b.Property<string>("CardEmail");

                    b.Property<string>("CardExpMonth");

                    b.Property<string>("CardExpYear");

                    b.Property<string>("CardMessage");

                    b.Property<string>("CardName");

                    b.Property<string>("CardNumber");

                    b.Property<string>("CardPIN");

                    b.Property<string>("CardRef");

                    b.Property<string>("CardType");

                    b.Property<string>("CardUrl");

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("LastDebited");

                    b.Property<string>("Token");

                    b.Property<string>("TransID");

                    b.Property<int>("TransactionCount");

                    b.Property<string>("UserEmail");

                    b.Property<Guid>("UserId");

                    b.Property<bool>("Valid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CardDetail");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.NextOfKin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("NextOfKin");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.OTPValidation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("OTPValidation");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.TransactionHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("CardExpMonth");

                    b.Property<string>("CardExpYear");

                    b.Property<string>("CardName");

                    b.Property<string>("CardNumber");

                    b.Property<string>("CardType");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("DestinationPhone");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Receipient");

                    b.Property<int>("Status");

                    b.Property<int>("TransactionType");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<bool>("Claimed");

                    b.Property<string>("Code");

                    b.Property<string>("Narration");

                    b.Property<Guid?>("ReceiverId");

                    b.Property<string>("ReceiverPhone");

                    b.Property<DateTime>("SendDate");

                    b.Property<Guid>("SenderId");

                    b.Property<string>("SenderPhone");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Transfer");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowAccountActivityNotif");

                    b.Property<bool>("AllowTransactionNotif");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("DOB");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("HasTransactionPin");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastWalletFundedDate");

                    b.Property<string>("MothersMedianName");

                    b.Property<string>("Passport");

                    b.Property<string>("Phone");

                    b.Property<int>("Pin");

                    b.Property<int>("TwoFactor");

                    b.Property<string>("UtilityBill");

                    b.Property<string>("ValidId");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.UserTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("CVV");

                    b.Property<string>("CardExpMonth");

                    b.Property<string>("CardExpYear");

                    b.Property<string>("CardName");

                    b.Property<string>("CardNumber");

                    b.Property<string>("CardType");

                    b.Property<bool>("IsAddCard");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<string>("TransactionReference");

                    b.Property<int>("TransactionStatus");

                    b.Property<int>("TransactionType");

                    b.Property<string>("UserEmail");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTransaction");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AvailableBalance");

                    b.Property<decimal>("CurrentBallance");

                    b.Property<DateTime>("DateCreated");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.Withdrawal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("RequestId");

                    b.Property<string>("TransactionId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Withdrawal");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FlashMoneyApi.Models.ActivityModel", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.AirtimeRecharge", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User", "User")
                        .WithMany("AirtimeRecharges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.CardDetail", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User", "User")
                        .WithMany("CardDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.NextOfKin", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User")
                        .WithOne("NextOfKin")
                        .HasForeignKey("FlashMoneyApi.Models.NextOfKin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.UserTransaction", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User", "User")
                        .WithMany("UserTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.Wallet", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("FlashMoneyApi.Models.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlashMoneyApi.Models.Withdrawal", b =>
                {
                    b.HasOne("FlashMoneyApi.Models.User", "User")
                        .WithMany("Withdrawals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FlashMoneyApi.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FlashMoneyApi.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlashMoneyApi.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FlashMoneyApi.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
