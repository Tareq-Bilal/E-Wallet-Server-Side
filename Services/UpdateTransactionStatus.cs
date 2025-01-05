using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services.Transactoin_Operations;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace E_Wallet_Server_Side.Services
{
    public class UpdateTransactionStatus
    {
        private static void _ExcuteTransaction(string TransactoinType , int WalletID, decimal Amount)
        {
            switch (TransactoinType)
            {
                case "Deposite":
                    {
                        ExcuteTransaction.Excute(TransactoinType, WalletID, Amount); break;
                    }

                case "Withdraw":
                    {
                        ExcuteTransaction.Excute(TransactoinType, WalletID, Amount); break;
                    }

                case "Payment":
                    {
                        ExcuteTransaction.Excute(TransactoinType, WalletID, Amount); break;
                    }

                case "Refund":
                    {
                        ExcuteTransaction.Excute(TransactoinType, WalletID, Amount); break;
                    }

            }
        }
        private static void _ChangeWalletBalance(int WalletID , decimal Amount , string TransactoinType , string OldStatus , string NewStatus)
        {
            //PENDING → COMPLETED, FAILED, CANCELLED
            //FAILED → PENDING(for retry attempts)
            //COMPLETED → No further changes(final state)
            //CANCELLED → No further changes(final state)
            
            if (OldStatus == "PENDING" && NewStatus == "COMPLETED")
            {
                _ExcuteTransaction(TransactoinType, WalletID, Amount);
            }
            
        }

        public static void UpdateStatus(int TransactoinID, string NewSatus)
        {
            using (var context = new AppDbContext())
            {
                var transactoin = context.Transactions.SingleOrDefault(t => t.transaction_id == TransactoinID);

                if (transactoin != null)
                {

                    _ChangeWalletBalance(transactoin.wallet_id , transactoin.amount , transactoin.transaction_type , transactoin.status , NewSatus);
                     transactoin.UpdateStatus(NewSatus);
                     context.SaveChanges();

                }

            }
        }
    }
}
