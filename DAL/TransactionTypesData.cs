using E_Wallet_Server_Side.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL
{
    public class TransactionTypesData
    {
        public static async Task<List<E_Wallet_Server_Side.Entities.TransactionTypes>> GetAllTransactionTypes()
        {
            using (var context = new AppDbContext())
            {
                return await context.TransactionTypes.ToListAsync();
            }
        }

    }
}
