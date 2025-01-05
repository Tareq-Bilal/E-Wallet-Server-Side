using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace E_Wallet_Server_Side.DAL
{
    public class WalletsData
    {
    
        public static async Task<List<WalletDTO>> GetAllWallets()
        {
                using (var context = new AppDbContext())
                {
                    var wallets = await context.Wallets_View
                    .FromSqlRaw("EXEC SP_GetWalletsFromWalletsView")  // Replace 'GetWallets' with your stored procedure name
                    .ToListAsync();
                    return wallets;
                }

        }
        public static WalletDTO GetAllWalletByUserName(string UserName)
        {
            using (var context = new AppDbContext())
            {
                var wallet = context.Wallets_View.SingleOrDefault(w => w.username == UserName);
                return wallet;
            }

        }

        public static WalletDTO GetAllWalletByID(int WalletID)
        {
            using (var context = new AppDbContext())
            {
                var wallet = context.Wallets_View.SingleOrDefault(w => w.ID == WalletID);
                return wallet;
            }

        }

        public static bool IsWalletExist(int WalletID)
        {
            using (var context = new AppDbContext())
            {
                return context.Wallets.Any(w => w.ID == WalletID);
            }
        }
        public static void AddNewWallet(Wallet NewWallet)
        {
            using (var context = new AppDbContext())
            {
                context.Add(NewWallet);
                context.SaveChanges();
            }
        }
        public static async Task UpdateWallet(UpdateWalletDTO UpdatedWallet)
        {
            using (var context = new AppDbContext())
            {
                var WalletIdParam = new SqlParameter("@WalletID", UpdatedWallet.ID);
                var userIdParam   = new SqlParameter("@user_id" , UpdatedWallet.user_id);
                var BalanceParam  = new SqlParameter("@Balance", UpdatedWallet.balance);
                var CurrencyParam = new SqlParameter("@Currency", UpdatedWallet.currency.ToUpper());
                await context.Database.ExecuteSqlRawAsync("EXEC SP_UpdateWallet @WalletID , @user_id , @Balance , @Currency",
                                                           WalletIdParam , userIdParam , BalanceParam , CurrencyParam);

                context.SaveChanges();
            }

        }


        public static void DeleteWallet(int WalletID)
        {
            using (var context = new AppDbContext())
            {
                var wallet = context.Wallets.SingleOrDefault(w => w.ID == WalletID);
                context.Remove(wallet);
                context.SaveChanges();
            }
        }

    }
}
