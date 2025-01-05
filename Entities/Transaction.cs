using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class Transaction
    {
        public int transaction_id { get; private set; }
        public int wallet_id { get; private set; }
        public string status { get; private set; }
        public string transaction_type { get; private set; }
        public decimal amount { get; private set; }
        public DateTime transaction_date { get; private set; }

        private Transaction() { }

        public Transaction(int transactionId, int walletID, string Status, string transactionType, decimal Amount, DateTime transactionDate)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));

            transaction_id = transactionId;
            wallet_id = walletID;
            transaction_type = transactionType;
            amount = Amount;
            transaction_date = transactionDate;
            status = Status;
        }

        public Transaction(DTOs.Transaction.TransactionDTO transactionDTO)
        {
            wallet_id = transactionDTO.wallet_id;
            transaction_type = transactionDTO.transaction_type;
            amount = transactionDTO.amount;
            transaction_date = transactionDTO.transaction_date;
            status = transactionDTO.status;
        }

        public void UpdateStatus(string newStatus)
        {
           this.status = newStatus;
        }

        public class TransactionConfiguration : IEntityTypeConfiguration<E_Wallet_Server_Side.Entities.Transaction>
        {
            public void Configure(EntityTypeBuilder<E_Wallet_Server_Side.Entities.Transaction> builder)
            {
                builder.Property(t => t.amount)
                       .HasColumnName("amount")
                       .IsRequired()
                       .HasPrecision(10, 2);

                builder.Property(t => t.transaction_date)
                       .IsRequired();

                builder.Property(t => t.status)
                       .HasColumnName("status")
                       .HasMaxLength(50)
                       .IsRequired();

                builder.HasKey(t => t.transaction_id);

                builder.ToTable("Transactions");

                builder.Property(t => t.transaction_id)
                       .ValueGeneratedOnAdd();
            }
        }
    }
}