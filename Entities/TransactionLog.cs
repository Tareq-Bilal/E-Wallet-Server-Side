using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class TransactionLog
    {
        public int id { get; private set; }
        public int transaction_id { get; private set; }
        public string status { get; private set; }
        public int? action_by { get; private set; }
        public string transaction_type { get; private set; }
        public string message { get; private set; }
        public DateTimeOffset created_at { get; private set; }

        private TransactionLog() { }

        public TransactionLog(DTOs.TransactionLog.TransactionLogDTO dto)
        {
            transaction_id = dto.transaction_id;
            status = dto.status;
            action_by = dto.action_by;
            transaction_type = dto.transaction_type;
            message = dto.message;
            created_at = dto.created_at;
        }

        public class TransactionLogConfiguration : IEntityTypeConfiguration<TransactionLog>
        {
            public void Configure(EntityTypeBuilder<TransactionLog> builder)
            {
                builder.ToTable("Transactionslogs");

                builder.HasKey(t => t.id);

                builder.Property(t => t.id)
                       .ValueGeneratedOnAdd();

                builder.Property(t => t.status)
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(t => t.transaction_type)
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(t => t.message)
                       .IsRequired()
                       .HasMaxLength(500);
            }
        }
    }
}
