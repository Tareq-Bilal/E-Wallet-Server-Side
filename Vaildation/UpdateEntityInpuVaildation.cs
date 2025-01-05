namespace E_Wallet_Server_Side.Vaildation
{
    public class UpdateEntityInpuVaildation
    {
        private readonly IUserRepository _userRepository;

        public UpdateEntityInpuVaildation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static bool _WalletEmptyPropertiesCheck(DTOs.Wallet.UpdateWalletDTO UpdatedWallet)
        {
            return (!string.IsNullOrEmpty(UpdatedWallet.currency)
             && UpdatedWallet.balance >= 0 && UpdatedWallet.user_id >= 1);

        }

        private static bool _UserEmptyPropertiesCheck(DTOs.UserDTO newUser)
        {
            return (!string.IsNullOrEmpty(newUser.userName)
                   && !string.IsNullOrEmpty(newUser.email)
                   && !string.IsNullOrEmpty(newUser.password_hash));
        }
        public static bool IsValidInput(object input)
        {

            if (input is DTOs.UserDTO UpdatedUser)
            {
                return (_UserEmptyPropertiesCheck(UpdatedUser));
            }

            if(input is DTOs.Wallet.UpdateWalletDTO UpdatedWallet)
            {
                return (_WalletEmptyPropertiesCheck(UpdatedWallet));
            }

            return false;
        }
    }
}
