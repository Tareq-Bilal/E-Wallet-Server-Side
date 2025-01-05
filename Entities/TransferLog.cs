using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class TransferLog
    {
        public int id { get; set; }
        public int transfer_id { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }

        public int action_by { get; set; }

        private TransferLog() { }

        public class TransferLogConfiguration : IEntityTypeConfiguration<TransferLog>
        {
            public void Configure(EntityTypeBuilder<TransferLog> builder)
            {
                // Table name
                builder.ToTable("TransferLogs");

                // Primary key
                builder.HasKey(t => t.id);

                // Properties
                builder.Property(t => t.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd(); // Identity column

                builder.Property(t => t.transfer_id)
                    .HasColumnName("transfer_id")
                    .IsRequired();

                builder.Property(t => t.status)
                    .HasColumnName("status")
                    .IsRequired()
                    .HasColumnType("nvarchar(nvarchar(20))");

                builder.Property(t => t.message)
                    .HasColumnName("message")
                    .HasColumnType("nvarchar(max)");

                builder.Property(t => t.created_at)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                builder.Property(t => t.action_by)
                       .HasColumnName("action_by")
                       .IsRequired();


                // Relationships
                //  builder.HasOne<Transfer>() // Assuming Transfer is another entity
                //      .WithMany() // If TransferLog doesn't have a navigation property in Transfer, use WithMany()
                //      .HasForeignKey(t => t.transfer_id)
                //      .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a transfer is removed
            }
        }

    }

}
