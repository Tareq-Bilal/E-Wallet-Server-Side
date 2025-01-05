namespace E_Wallet_Server_Side
{
    public class VaildTransactionStatuses
    {
        public static readonly HashSet<string> ValidStatuses = new()
        {   
            "PENDING",
            "COMPLETED",
            "FAILED",
            "CANCELLED"
        };


    }
}
