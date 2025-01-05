using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL
{
    public class TransfaresData
    {
        public static async Task<List<Transfer>> GetAllTransfers()
        {
            using (var context = new AppDbContext())
            {
                return await context.Transfers.ToListAsync();
            }
        }
    }
}
