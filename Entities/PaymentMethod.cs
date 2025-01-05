using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_Wallet_Server_Side.Entities;
using System;
using E_Wallet_Server_Side.DTOs.PaymentMethod;

namespace E_Wallet_Server_Side.Entities
{
    public class PaymentMethod
    {
        // =====> Examplles Of Payment Method Deatails :
//        -- Credit Card - just store token and last4 for identification
//{"processor_token": "tok_visa_4242", "last4": "4242", "brand": "Visa", "exp_month": "12", "exp_year": "2025"}

//-- Bank Account - minimal bank details with token
//{"processor_token": "ba_1234", "bank_name": "Chase", "last4": "5678", "type": "checking"}

//--PayPal
//{ "email": "user@example.com", "paypal_token": "PP_TOKEN_123"}

//--Google Pay
//{"email: user@gmail.com, device_token": GPAY_TOK_123"}

//--Apple Pay
//{"device_token : APAY_TOK_123 , device : iPhone"}

//--Crypto Wallet
//{"wallet_address : 0x1234...5678, currency : ETH "}
        public int id { get; set; }
        public int user_id { get; set; }
        public string method_type { get; set; }
        public string details { get; set; }
        public DateTime created_at { get; set; }

        private PaymentMethod() { }

        public PaymentMethod(PaymentMethodDTO paymentMethodDTO) { 
        
            user_id = paymentMethodDTO.user_id;
            method_type = paymentMethodDTO.method_type;
            details = paymentMethodDTO.details;
            created_at = DateTime.UtcNow;

        }
        public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
        {
            public void Configure(EntityTypeBuilder<PaymentMethod> builder)
            {
                // Table name
                builder.ToTable("Payment_Methods");

                // Primary key
                builder.HasKey(p => p.id);

                // Properties
                builder.Property(p => p.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd(); // Identity column

                builder.Property(p => p.user_id)
                    .HasColumnName("user_id")
                    .IsRequired();

                builder.Property(p => p.method_type)
                    .HasColumnName("method_type")
                    .IsRequired()
                    .HasMaxLength(50); // Define max length as per your requirements

                builder.Property(p => p.details)
                    .HasColumnName("details")
                    .HasColumnType("nvarchar(max)");

                builder.Property(p => p.created_at)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                // Relationships
                builder.HasOne<User>() // Assuming `User` is the related entity
                    .WithMany() // Adjust if there is a navigation property in `User`
                    .HasForeignKey(p => p.user_id)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a user is removed
            }
        }



    }
}
