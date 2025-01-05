using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.DAL;

namespace E_Wallet_Server_Side.BL
{
    public static class TransfersLogsAPIBusiness
    {
        public static async Task<List<TransferLog>> GetTransfersLogsAsync()
        {
            return await TransfersLogsData.GetTransfersLogsAsync();
        }
    }
}
