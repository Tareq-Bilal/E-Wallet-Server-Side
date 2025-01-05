namespace E_Wallet_Server_Side.DTOs.Wallet
{
    public class AddWalletDTO
    {
        public int ID { get; set; }
        public int user_id { get; set; }
        public decimal balance { get;  set; }
        public string currency { get; set; }
        public DateTime created_at { get; set; }

    }
}
