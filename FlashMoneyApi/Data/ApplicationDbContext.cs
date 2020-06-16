using EducationAccessApi.Models;
using FlashMoneyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<FlashMoneyApi.Models.User> User { get; set; }
        public DbSet<FlashMoneyApi.Models.Wallet> Wallet { get; set; }
        public DbSet<FlashMoneyApi.Models.CardAddProcess> CardAddProcess { get; set; }
        public DbSet<FlashMoneyApi.Models.CardDetail> CardDetail { get; set; }

        public DbSet<FlashMoneyApi.Models.UserTransaction> UserTransaction { get; set; }
        public DbSet<FlashMoneyApi.Models.Transfer> Transfer { get; set; }
        public DbSet<FlashMoneyApi.Models.OTPValidation> OTPValidation { get; set; }
        public DbSet<FlashMoneyApi.Models.Withdrawal> Withdrawal { get; set; }
        public DbSet<FlashMoneyApi.Models.AirtimeRecharge> AirtimeRecharge { get; set; }
        public DbSet<FlashMoneyApi.Models.TransactionHistory> TransactionHistory { get; set; }
        public DbSet<FlashMoneyApi.Models.ActivityModel> ActivityModel { get; set; }
        public DbSet<FlashMoneyApi.Models.NextOfKin> NextOfKin { get; set; }
        public DbSet<FlashMoneyApi.Models.Login> Logins { get; set; }
        public DbSet<ConfirmationAwaiting> ConfirmationAwaitings { get; set; }
    }
}
