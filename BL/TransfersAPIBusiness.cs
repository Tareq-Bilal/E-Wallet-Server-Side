using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.DAL;

namespace E_Wallet_Server_Side.BL
{
    public class TransfersAPIBusiness
    {
        public static async Task<List<Transfer>> GetAllTransfers()
        {
            return await TransfaresData.GetAllTransfers();
        }

    }
}
