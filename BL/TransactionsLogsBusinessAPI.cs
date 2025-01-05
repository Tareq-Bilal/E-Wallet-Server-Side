using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side.BL
{
    public class TransactionsLogsBusinessAPI
    {
        public static async Task<List<TransactionLog>> GetAllTransactionsLog()
        {
            return await TransactionsLogsData.GetAllTransactionsLog();
        }
    }
}
