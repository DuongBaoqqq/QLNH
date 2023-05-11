using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BankManagement.Models.EF
{
    public partial class BankManagementDbContext : DbContext
    {
        private static BankManagementDbContext _Instance;
        public static BankManagementDbContext Instance
        {
            get { return _Instance ?? (_Instance = new BankManagementDbContext()); }
            set { }
        }
        public BankManagementDbContext()
            : base("name=Model13")
        {
        }

        public virtual DbSet<ACCOUNT_CUSTOMER> ACCOUNT_CUSTOMER { get; set; }
        public virtual DbSet<ACCOUNT_LOCK_HISTORY> ACCOUNT_LOCK_HISTORY { get; set; }
        public virtual DbSet<ACCOUNT_NUMBER_BANK> ACCOUNT_NUMBER_BANK { get; set; }
        public virtual DbSet<ACCOUNT_NUMBER_CUSTOMER> ACCOUNT_NUMBER_CUSTOMER { get; set; }
        public virtual DbSet<ACCOUNT_STAFF> ACCOUNT_STAFF { get; set; }
        public virtual DbSet<AGENCY> AGENCies { get; set; }
        public virtual DbSet<BANK_RECEIVING_TRANSFER> BANK_RECEIVING_TRANSFER { get; set; }
        public virtual DbSet<BANK_TRANSFER_HISTORY> BANK_TRANSFER_HISTORY { get; set; }
        public virtual DbSet<CASH_RECEIVING_BANK> CASH_RECEIVING_BANK { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<CUSTOMER_LOGIN_HISTORY> CUSTOMER_LOGIN_HISTORY { get; set; }
        public virtual DbSet<CUSTOMER_TRANSACTION_HISTORY> CUSTOMER_TRANSACTION_HISTORY { get; set; }
        public virtual DbSet<DISTRICT> DISTRICTs { get; set; }
        public virtual DbSet<GET_SAVING> GET_SAVING { get; set; }
        public virtual DbSet<INTEREST_RATE> INTEREST_RATE { get; set; }
        public virtual DbSet<PROVINCE> PROVINCEs { get; set; }
        public virtual DbSet<SATFF_LOGIN_HISTORY> SATFF_LOGIN_HISTORY { get; set; }
        public virtual DbSet<SAVING> SAVINGs { get; set; }
        public virtual DbSet<STAFF> STAFFs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNT_CUSTOMER>()
                .Property(e => e.Pass)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_CUSTOMER>()
                .Property(e => e.PinCode)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_LOCK_HISTORY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_LOCK_HISTORY>()
                .Property(e => e.AccountNumber)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_NUMBER_BANK>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_NUMBER_BANK>()
                .Property(e => e.AccountNumber)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_NUMBER_BANK>()
                .HasMany(e => e.BANK_TRANSFER_HISTORY)
                .WithOptional(e => e.ACCOUNT_NUMBER_BANK)
                .HasForeignKey(e => e.AccountNumberTransfers);

            modelBuilder.Entity<ACCOUNT_NUMBER_BANK>()
                .HasMany(e => e.BANK_RECEIVING_TRANSFER)
                .WithOptional(e => e.ACCOUNT_NUMBER_BANK)
                .HasForeignKey(e => e.AccountNumberReceiving);

            modelBuilder.Entity<ACCOUNT_NUMBER_BANK>()
                .HasMany(e => e.CASH_RECEIVING_BANK)
                .WithOptional(e => e.ACCOUNT_NUMBER_BANK)
                .HasForeignKey(e => e.AccountNumberReceiving);

            modelBuilder.Entity<ACCOUNT_NUMBER_CUSTOMER>()
                .Property(e => e.AccountNumber)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_NUMBER_CUSTOMER>()
                .HasMany(e => e.CUSTOMER_TRANSACTION_HISTORY)
                .WithOptional(e => e.ACCOUNT_NUMBER_CUSTOMER)
                .HasForeignKey(e => e.AccountNumberTransfers);

            modelBuilder.Entity<ACCOUNT_NUMBER_CUSTOMER>()
                .HasMany(e => e.BANK_RECEIVING_TRANSFER)
                .WithOptional(e => e.ACCOUNT_NUMBER_CUSTOMER)
                .HasForeignKey(e => e.AccountNumberTransfers);

            modelBuilder.Entity<ACCOUNT_NUMBER_CUSTOMER>()
                .HasMany(e => e.CUSTOMER_TRANSACTION_HISTORY1)
                .WithOptional(e => e.ACCOUNT_NUMBER_CUSTOMER1)
                .HasForeignKey(e => e.AccountNumberReceiving);

            modelBuilder.Entity<ACCOUNT_NUMBER_CUSTOMER>()
                .HasMany(e => e.BANK_TRANSFER_HISTORY)
                .WithOptional(e => e.ACCOUNT_NUMBER_CUSTOMER)
                .HasForeignKey(e => e.AccountNumberReceiving);

            modelBuilder.Entity<ACCOUNT_STAFF>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_STAFF>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_STAFF>()
                .Property(e => e.Pass)
                .IsFixedLength();

            modelBuilder.Entity<ACCOUNT_STAFF>()
                .HasMany(e => e.SATFF_LOGIN_HISTORY)
                .WithOptional(e => e.ACCOUNT_STAFF)
                .HasForeignKey(e => e.IdStaff);

            modelBuilder.Entity<AGENCY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<AGENCY>()
                .Property(e => e.NameAgency)
                .IsFixedLength();

            modelBuilder.Entity<AGENCY>()
                .Property(e => e.AddressAgenCy)
                .IsFixedLength();

            modelBuilder.Entity<AGENCY>()
                .HasMany(e => e.CUSTOMERs)
                .WithOptional(e => e.AGENCY)
                .HasForeignKey(e => e.IdAgency);

            modelBuilder.Entity<AGENCY>()
                .HasMany(e => e.STAFFs)
                .WithOptional(e => e.AGENCY)
                .HasForeignKey(e => e.IdAgency);

            modelBuilder.Entity<BANK_RECEIVING_TRANSFER>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<BANK_RECEIVING_TRANSFER>()
                .Property(e => e.AccountNumberTransfers)
                .IsFixedLength();

            modelBuilder.Entity<BANK_RECEIVING_TRANSFER>()
                .Property(e => e.AccountNumberReceiving)
                .IsFixedLength();

            modelBuilder.Entity<BANK_RECEIVING_TRANSFER>()
                .Property(e => e.Content)
                .IsFixedLength();

            modelBuilder.Entity<BANK_TRANSFER_HISTORY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<BANK_TRANSFER_HISTORY>()
                .Property(e => e.AccountNumberTransfers)
                .IsFixedLength();

            modelBuilder.Entity<BANK_TRANSFER_HISTORY>()
                .Property(e => e.AccountNumberReceiving)
                .IsFixedLength();

            modelBuilder.Entity<BANK_TRANSFER_HISTORY>()
                .Property(e => e.Content)
                .IsFixedLength();

            modelBuilder.Entity<CASH_RECEIVING_BANK>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<CASH_RECEIVING_BANK>()
                .Property(e => e.AccountNumberReceiving)
                .IsFixedLength();

            modelBuilder.Entity<CASH_RECEIVING_BANK>()
                .Property(e => e.Content)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.NameCustomer)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.IdCard)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.AddressCustomer)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.IdAgency)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER>()
                .HasOptional(e => e.ACCOUNT_CUSTOMER)
                .WithRequired(e => e.CUSTOMER);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.CUSTOMER_LOGIN_HISTORY)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.IdCustomer);

            modelBuilder.Entity<CUSTOMER_LOGIN_HISTORY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER_TRANSACTION_HISTORY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER_TRANSACTION_HISTORY>()
                .Property(e => e.AccountNumberTransfers)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER_TRANSACTION_HISTORY>()
                .Property(e => e.AccountNumberReceiving)
                .IsFixedLength();

            modelBuilder.Entity<CUSTOMER_TRANSACTION_HISTORY>()
                .Property(e => e.Content)
                .IsFixedLength();

            modelBuilder.Entity<DISTRICT>()
                .HasMany(e => e.AGENCies)
                .WithOptional(e => e.DISTRICT)
                .HasForeignKey(e => new { e.ID_Province, e.ID_District });

            modelBuilder.Entity<DISTRICT>()
                .HasMany(e => e.CUSTOMERs)
                .WithOptional(e => e.DISTRICT)
                .HasForeignKey(e => new { e.ID_Province, e.ID_District });

            modelBuilder.Entity<DISTRICT>()
                .HasMany(e => e.STAFFs)
                .WithOptional(e => e.DISTRICT)
                .HasForeignKey(e => new { e.ID_Province, e.ID_District });

            modelBuilder.Entity<GET_SAVING>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<GET_SAVING>()
                .Property(e => e.AccountNumber)
                .IsFixedLength();

            modelBuilder.Entity<GET_SAVING>()
                .Property(e => e.IdSaving)
                .IsFixedLength();

            modelBuilder.Entity<INTEREST_RATE>()
                .Property(e => e.Term)
                .IsFixedLength();

            modelBuilder.Entity<INTEREST_RATE>()
                .HasMany(e => e.SAVINGs)
                .WithOptional(e => e.INTEREST_RATE)
                .HasForeignKey(e => e.IdInterestRate);

            modelBuilder.Entity<PROVINCE>()
                .HasMany(e => e.DISTRICTs)
                .WithRequired(e => e.PROVINCE)
                .HasForeignKey(e => e.ID_Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SATFF_LOGIN_HISTORY>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<SATFF_LOGIN_HISTORY>()
                .Property(e => e.IdStaff)
                .IsFixedLength();

            modelBuilder.Entity<SAVING>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<SAVING>()
                .Property(e => e.AccountNumber)
                .IsFixedLength();

            modelBuilder.Entity<SAVING>()
                .HasMany(e => e.GET_SAVING)
                .WithOptional(e => e.SAVING)
                .HasForeignKey(e => e.IdSaving);

            modelBuilder.Entity<STAFF>()
                .Property(e => e.NameStaff)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .Property(e => e.IdCard)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .Property(e => e.AddressStaff)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .Property(e => e.IdAgency)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<STAFF>()
                .HasOptional(e => e.ACCOUNT_STAFF)
                .WithRequired(e => e.STAFF);
        }
    }
}
