using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side.BL
{
    public class WalletAPIBusiness
    {
        public static async Task<List<WalletDTO>> GetAllWallets()
        {
            List<WalletDTO> wallets =  await WalletsData.GetAllWallets();
            return wallets;
        }
        public static WalletDTO GetAllWalletByUserName(string UserName)
        {
            return WalletsData.GetAllWalletByUserName(UserName);
        }
        public static WalletDTO GetAllWalletByID(int WalletID)
        {
            return WalletsData.GetAllWalletByID(WalletID);
        }

        public static bool IsWalletExist(int WalletID)
        {
            return WalletsData.IsWalletExist(WalletID);
        }
        public static void AddNewWallet(Wallet NewWallet)
        {
            WalletsData.AddNewWallet(NewWallet);
        }
        public static async Task UpdateWallet(UpdateWalletDTO UpdatedWallet)
        {
            WalletsData.UpdateWallet(UpdatedWallet);
        }

        public static void DeleteWallet(int WalletID)
        {
            WalletsData.DeleteWallet(WalletID);
        }
    }
}
