using E_Wallet_Server_Side.DAL;

namespace E_Wallet_Server_Side.BL
{
    public class TransactionAPIBusiness
    {

        public static async Task<List<E_Wallet_Server_Side.Entities.Transaction>> GetAllTransactions()
        {
            return await TransactoinsData.GetAllTransactions();
        }

        public static async Task AddTransactoin(E_Wallet_Server_Side.Entities.Transaction NewTransaction)
        {
            TransactoinsData.AddTransactoin(NewTransaction);
        }

    }
}
