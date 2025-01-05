using E_Wallet_Server_Side.DTOs.Transfer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class Transfer
    {
        public int id { get; private set; }

        public int sender_wallet_id { get; private set; }

        public int receiver_wallet_id { get; private set; }

        public decimal amount { get; private set; }

        public string description { get; private set; }

        public DateTime created_at { get; private set; }

        //public int action_by { get; private set; }


        private Transfer() { }

        public Transfer(TransferDTO transferDTO)
        {
            sender_wallet_id = transferDTO.sender_wallet_id;
            receiver_wallet_id = transferDTO.receiver_wallet_id;
            amount = transferDTO.amount;
            description = transferDTO.description;
            created_at = DateTime.UtcNow;
         //   action_by = transferDTO.action_by;
        }

        public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
        {
            public void Configure(EntityTypeBuilder<Transfer> builder)
            {
                builder.ToTable("Transfers");

                builder.HasKey(t => t.id);

                builder.Property(t => t.id)
                       .HasColumnName("id")
                       .ValueGeneratedOnAdd();

                builder.Property(t => t.sender_wallet_id)
                       .HasColumnName("sender_wallet_id")
                       .IsRequired();

                builder.Property(t => t.receiver_wallet_id)
                       .HasColumnName("receiver_wallet_id")
                       .IsRequired();

                builder.Property(t => t.amount)
                       .HasColumnName("amount")
                       .IsRequired()
                       .HasPrecision(10, 2);

                builder.Property(t => t.description)
                       .HasColumnName("description")
                       .HasMaxLength(500);

                builder.Property(t => t.created_at)
                       .HasColumnName("created_at")
                       .IsRequired();


            }
        }

    }
}
