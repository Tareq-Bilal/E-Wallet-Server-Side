using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.DTOs.PaymentMethod;
using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL
{
    public static class PaymentMethodData
    {
        public static async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            using (var context = new AppDbContext())
            {

                return await context.PaymentMethods.ToListAsync();
            }
        }
        public static async Task AddUserPaymentMethod(PaymentMethod paymentMethod)
        {
            using (var context = new AppDbContext())
            {
                context.Add(paymentMethod);
                context.SaveChanges();
            }
        }

        public static async Task<bool> UpdateUserPaymentMethod(int id, PaymentMethodUpdateDto updateDto)
        {
            // Find existing payment method

            using (var context = new AppDbContext())
            {
                 var paymentMethod = await context.PaymentMethods
                .FirstOrDefaultAsync(p => p.id == id && p.user_id == updateDto.user_id);

                if (paymentMethod == null)
                    return false;

                // Update properties
                paymentMethod.method_type = updateDto.method_type;
                paymentMethod.details = updateDto.details;

                 try
                 {
                    await context.SaveChangesAsync();
                    return true;
                 }

                 catch
                 {
                    return false;
                 }

        }
     }

        public static async Task<bool> DeleteUserPaymentMethod(int id)
        {
            try
            {
                using var context = new AppDbContext();
                var paymentMethod = await context.PaymentMethods
                    .FirstOrDefaultAsync(p => p.id == id);

                if (paymentMethod == null)
                {
                    return false; // Payment method not found
                }

                context.PaymentMethods.Remove(paymentMethod);
                var rowsAffected = await context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details here
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception details here
                throw; // Re-throw unexpected exceptions
            }
        }


    }
}
