using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DTOs.Wallet
{
    public class WalletDTO
    {
        public int ID { get; set; }
        public string username { get; set; }
        public decimal balance { get; private set; }
        public string currency { get; set; }
        public DateTime created_at { get; set; }

        public class WalletDTOConfiguration : IEntityTypeConfiguration<WalletDTO>
        {
            public void Configure(EntityTypeBuilder<WalletDTO> builder)
            {
                builder.Property(w => w.balance)
                       .HasColumnName("balance")
                       .IsRequired();

            }
        }

    }
}
