namespace E_Wallet_Server_Side.DTOs.PaymentMethod
{
    public class PaymentMethodUpdateDto
    {
        public int user_id { get; set; }  // For security verification
        public string method_type { get; set; }
        public string details { get; set; }
    }
}
