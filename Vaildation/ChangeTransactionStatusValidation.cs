using E_Wallet_Server_Side.BL;

namespace E_Wallet_Server_Side.Vaildation
{
    public class ChangeTransactionStatusValidation
    {
        public static bool IsVaildStatusChange(string FromStatus , string ToStatus)
        {
            switch (FromStatus)
            {
                case "PENDING":
                    return true;

                case "FAILED":
                    {
                        if(ToStatus == "PENDING")
                            return true;

                        return false;

                    }

                case "COMPLETED":
                    return false;

                case "CANCELLED":
                    return false;

            }   

            return false;
        }
    }
}
