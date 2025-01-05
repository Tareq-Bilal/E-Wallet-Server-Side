using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services.Transactoin_Operations;

namespace E_Wallet_Server_Side.Vaildation
{
    public static class PaymentAndWithdrawAmountValidation
    {
        public static bool IsBalanceEnoughForTransaction(int WalletID , decimal Amount)
        {
              var Wallet = WalletAPIBusiness.GetAllWalletByID(WalletID);
                return (Wallet.balance > Amount);

        }
    }
}
