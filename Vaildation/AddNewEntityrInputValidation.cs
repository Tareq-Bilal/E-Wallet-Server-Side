namespace E_Wallet_Server_Side.Vaildation
{
    public class AddNewEntityrInputValidation 
    {
        private readonly IUserRepository _userRepository;

        public AddNewEntityrInputValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static bool _UserEmptyPropertiesCheck(DTOs.UserDTO newUser)
        {
            return (!string.IsNullOrEmpty(newUser.userName)
                   && !string.IsNullOrEmpty(newUser.email)
                   && !string.IsNullOrEmpty(newUser.password_hash));
        }

        public static bool IsValidInput(object input)
        {

            if(input is DTOs.UserDTO newUser)
            {
                return (_UserEmptyPropertiesCheck(newUser));
            }

            return false;
        }

    }
}
