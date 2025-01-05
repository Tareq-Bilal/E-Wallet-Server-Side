using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.DTOs.Wallet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace E_Wallet_Server_Side.DAL
{
    public class TransactoinsData
    {
        public static async Task<List<E_Wallet_Server_Side.Entities.Transaction>> GetAllTransactions()
        {
            using (var context = new AppDbContext())
            {
                return await context.Transactions.ToListAsync();
            }
        }
        public static async Task AddTransactoin(E_Wallet_Server_Side.Entities.Transaction NewTransaction)
        {
            using (var context = new AppDbContext())
            {
                context.Add(NewTransaction);
                context.SaveChanges();
            }
        }
    }
}
