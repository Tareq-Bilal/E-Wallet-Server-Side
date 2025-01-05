using E_Wallet_Server_Side.DTOs.Transaction;
using E_Wallet_Server_Side.Entities;

namespace E_Wallet_Server_Side.Services.TransactionsMessageGeneration
{
    public static class TransactionMessageGenerator
    {

        public static string GenerateTransactionMessage(string status , string transaction_type , decimal amount , int wallet_id)
        {
            switch (status)
            {

                case "PENDING":
                    return $"{transaction_type} of {amount}$ for Wallet With ID [{wallet_id}] is Pending";

                case "COMPLETED":
                    return $"{amount}$ {transaction_type} Transactoin Completed Successfully  For Wallet With ID [{wallet_id}]";

                case "CANCELLED":
                    return $"{transaction_type}  of {amount}$ for Wallet With ID [{wallet_id}] Has Canceled";

                case "FAILED":
                    return $"{amount}$  {transaction_type}  Transactoin Failed to Excute To Wallet With ID [{wallet_id}] ! , Please Try Again Or Contact Us";


            }
            return "PENDING";
        }
    }
}
