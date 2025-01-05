using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL

{
    public class TransactionsLogsData
    {

        public static async Task<List<TransactionLog>> GetAllTransactionsLog()
        {

            using (var context = new AppDbContext())
            {
                return await context.TransactionLogs.ToListAsync();
            }

        }


    }
}
