using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side.Services.Transactions_Logging
{
    public static class TransactionLoggingService
    {
        public static void LogTransaction(DTOs.TransactionLog.TransactionLogDTO transactionLogDTO)
        {
            using (var context = new AppDbContext())
            {
                TransactionLog transactionLog = new TransactionLog(transactionLogDTO);
                context.TransactionLogs.Add(transactionLog);
                context.SaveChanges();
            }
        }

    }
}
