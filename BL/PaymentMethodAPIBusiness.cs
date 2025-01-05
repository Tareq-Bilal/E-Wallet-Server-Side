using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.DTOs.PaymentMethod;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side.BL
{
    public static class PaymentMethodAPIBusiness
    {
        public static async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await PaymentMethodData.GetPaymentMethodsAsync();
        }
        public static async Task AddUserPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethodData.AddUserPaymentMethod(paymentMethod);
        }

        public static async Task<bool> UpdatePaymentMethod(int id, PaymentMethodUpdateDto updateDto)
        {
            return await PaymentMethodData.UpdateUserPaymentMethod(id, updateDto);
        }
        public static async Task<bool> DeleteUserPaymentMethod(int id)
        {
            return await PaymentMethodData.DeleteUserPaymentMethod(id);
        }


    }
}
