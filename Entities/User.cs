using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.Entities
{
    public class User
    {

        public int ID { get; set; }
        public string userName { get; set; }

        public string email { get; set; }
        protected string password_hash { get; set; }

        public DateTime created_at { get; set; }

        public override string ToString()
        {
            return $"[{ID}]\t{userName}\t{email}\t{created_at:d}";
        }
        public void SetPassword(string Password)
        {
            this.password_hash = Password;
        }
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.Property(u => u.password_hash)
                       .HasColumnName("password_hash")
                       .IsRequired();
            }
        }

        public string GetPassword()
        {
            return this.password_hash;
        }

    }
}
