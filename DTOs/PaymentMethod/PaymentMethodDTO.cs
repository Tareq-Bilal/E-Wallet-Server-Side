namespace E_Wallet_Server_Side.DTOs.PaymentMethod
{
    public class PaymentMethodDTO
    {
        public int id { get; private set; }
        public int user_id { get; set; }
        public string method_type { get; set; }
        public string details { get; set; }


    }
}
