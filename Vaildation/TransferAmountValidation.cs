using E_Wallet_Server_Side.BL;

namespace E_Wallet_Server_Side.Vaildation
{
    public static class TransferAmountValidation
    {
        public static async Task<bool> IsValidTransferAmountAsync(int sender_wallet_id , int receiver_wallet_id)
        {
            var Wallets = await WalletAPIBusiness.GetAllWallets();  
            decimal SenderWalletBalance   = Wallets.Where(w => w.ID == sender_wallet_id).Select(tr => tr.balance).FirstOrDefault();
            decimal ReceiverWalletBalance = Wallets.Where(w => w.ID == receiver_wallet_id).Select(tr => tr.balance).FirstOrDefault();

            return SenderWalletBalance >= ReceiverWalletBalance;

        }
    }
}
