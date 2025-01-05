using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <User> Users { get; set; }
        public DbSet <Wallet> Wallets { get; set; }
        public DbSet<WalletDTO> Wallets_View { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.Transaction> Transactions { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.TransactionTypes> TransactionTypes { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.TransactionLog> TransactionLogs { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.Transfer> Transfers { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.TransferLog> TransferLogs { get; set; }
        public DbSet <E_Wallet_Server_Side.Entities.PaymentMethod> PaymentMethods { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

            var constr = config.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(constr);
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new User.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Wallet.WalletConfiguration());
            modelBuilder.ApplyConfiguration(new Transfer.TransferConfiguration());
            modelBuilder.ApplyConfiguration(new WalletDTO.WalletDTOConfiguration());
            modelBuilder.ApplyConfiguration(new TransferLog.TransferLogConfiguration());
            modelBuilder.ApplyConfiguration(new Transaction.TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethod.PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionLog.TransactionLogConfiguration());
        }

    }
}
