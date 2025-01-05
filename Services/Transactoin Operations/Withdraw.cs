using E_Wallet_Server_Side.Data;

namespace E_Wallet_Server_Side.Services.Transactoin_Operations
{
    public class Withdraw : ITransactoin
    {
        public void Excute(int WalletID ,decimal Amount)
        {
            using (var context = new AppDbContext())
            {
                var Wallet = context.Wallets.SingleOrDefault(w => w.ID == WalletID);

                Wallet.SetBalance(Wallet.GetBalance() - Amount);

                //This Transaction Log Should Be Done Here ...

                context.SaveChanges();

            }
        }
    }
}
