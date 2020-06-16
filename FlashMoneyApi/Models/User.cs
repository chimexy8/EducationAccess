using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string MothersMedianName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public int Pin { get; set; }
        public int TwoFactor { get; set; }
        public string Passport { get; set; }
        public string ValidId { get; set; }
        public string UtilityBill { get; set; }
        public bool HasTransactionPin { get; set; }
        public bool HasResetPin { get; set; }
        public bool AllowTransactionNotif { get; set; }
        public bool AllowAccountActivityNotif { get; set; }
        public DateTime LastWalletFundedDate { get; set; }
        public Wallet Wallet { get; set; }
        public NextOfKin NextOfKin { get; set; }
        public ICollection<CardDetail> CardDetails { get; set; }
        public ICollection<UserTransaction> UserTransactions { get; set; }
        public ICollection<Withdrawal> Withdrawals { get; set; }
        public ICollection<AirtimeRecharge> AirtimeRecharges { get; set; }
        public ICollection<ActivityModel> Activities { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
