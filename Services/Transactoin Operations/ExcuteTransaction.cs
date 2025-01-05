namespace E_Wallet_Server_Side.Services.Transactoin_Operations
{
    public static class ExcuteTransaction
    {
        public static void Excute(string TransactionType ,  int WalletID, decimal Amount)
        {
            ITransactoin _transactoin;

            switch (TransactionType)
            {

                case "Deposite":
                    {
                        _transactoin = new Deposite();
                        _transactoin.Excute(WalletID , Amount);
                        break;
                    }

                case "Withdraw":
                    {
                        _transactoin = new Withdraw();
                        _transactoin.Excute(WalletID, Amount);
                        break;
                    }
                case "Payment":
                    {
                        _transactoin = new Payment();
                        _transactoin.Excute(WalletID, Amount);
                        break;
                    }
                case "Refund":
                    {
                        _transactoin = new Refund();
                        _transactoin.Excute(WalletID, Amount);
                        break;
                    }
            }

        }
    }
}
