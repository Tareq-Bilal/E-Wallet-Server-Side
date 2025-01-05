using E_Wallet_Server_Side.DAL;

namespace E_Wallet_Server_Side.BL
{
    public class TransactoinTypesAPIBusiness
    {
        public static async Task<List<E_Wallet_Server_Side.Entities.TransactionTypes>> GetAllTransactionTypes()
        {
            return await TransactionTypesData.GetAllTransactionTypes();
        }

    }
}
