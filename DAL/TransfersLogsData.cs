using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL
{
    public static class TransfersLogsData
    {
        public static async Task<List<TransferLog>> GetTransfersLogsAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.TransferLogs.ToListAsync();
            }
        }
    }
}
