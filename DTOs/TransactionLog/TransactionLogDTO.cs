namespace E_Wallet_Server_Side.DTOs.TransactionLog
{
    public class TransactionLogDTO
    {
        public int id { get; set; }
        public int transaction_id { get; set; }
        public string status { get; set; }
        public int? action_by { get; set; }
        public string transaction_type { get; set; }
        public string message { get; set; }
        public DateTimeOffset created_at { get; set; }
    }
}
