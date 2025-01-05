namespace E_Wallet_Server_Side.DTOs.Transaction
{
    public class UpdateTransactionDTO
    {
        public int TransactionId { get; set; }
        //public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        protected bool IsSuccessful { get; set; }
        protected string Message { get; set; }

        public void SetIsSuccessfulSatatus(bool result)
        {
            IsSuccessful = result;  
        }

        public void SetMessage(string message)
        {
            Message = message;
        }


    }
}
