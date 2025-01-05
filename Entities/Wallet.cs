using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class Wallet
    {
        public int ID { get; set; }
        public int user_id { get; set; }
        protected decimal balance { get; private set; }
        public string currency { get; set; }
        public DateTime created_at { get; set; }

        public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
        {
            public void Configure(EntityTypeBuilder<Wallet> builder)
            {
                builder.Property(w => w.balance)
                       .HasColumnName("balance")
                       .IsRequired();
            }
        }

        public void SetBalance(decimal balance) {   this.balance = balance; }

        public decimal GetBalance() { return  this.balance; }


    }
}
