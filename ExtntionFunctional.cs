using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side
{
    public static class ExtntionFunctional
    {
        public static IEnumerable<WalletDTO> Filter(this IEnumerable<WalletDTO> wallets, Func<WalletDTO, bool> predicate)
        {

            foreach (var wallet in wallets)
            {
                if (predicate(wallet))
                {
                    yield return wallet;
                }
            }
        }
        public static IEnumerable<E_Wallet_Server_Side.Entities.Transaction> TransactionFilter(this IEnumerable<E_Wallet_Server_Side.Entities.Transaction> transactions, Func<E_Wallet_Server_Side.Entities.Transaction, bool> predicate)
        {

            foreach (var transaction in transactions)
            {
                if (predicate(transaction))
                {
                    yield return transaction;
                }
            }
        }


    }
}
