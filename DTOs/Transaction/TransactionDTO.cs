namespace E_Wallet_Server_Side.DTOs.Transaction
{
    public class TransactionDTO
    {
        public int transaction_id { get; set; }
        public int wallet_id { get;  set; }
        public string status { get; set; }
        public string transaction_type { get; set; }
        public decimal amount { get; set; }
        public DateTime transaction_date { get; set; }
    }
}
