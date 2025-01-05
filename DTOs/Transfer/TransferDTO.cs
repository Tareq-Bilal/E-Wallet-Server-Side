namespace E_Wallet_Server_Side.DTOs.Transfer
{
    public class TransferDTO
    {

        public int sender_wallet_id { get; set; }

        public int receiver_wallet_id { get; set; }

        public decimal amount { get; set; }

        public string description { get; set; }

        protected int action_by { get; private set; }

        public void SetActionby(int id)
        {
            action_by = id;
        }
        public int GetActionby()
        {
            return action_by;
        }

    }
}
