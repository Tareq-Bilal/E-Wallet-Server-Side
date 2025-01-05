using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.DTOs;

namespace E_Wallet_Server_Side.BL
{
    public class UserAPIBusiness
    {
        public static List<Entities.User> GetAllUsers()
        {
            return UsersData.GetAllUsers();
        }

        public static Entities.User GetUserByID(int UserId)
        {
            return UsersData.GetUserByID(UserId);
        }
        public static Entities.User GetUserByName(string UserName)
        {
            return UsersData.GetUserByName(UserName);
        }

        public static bool IsUserExist(int UserID)
        {
            return UsersData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return UsersData.IsUserExist(UserName);
        }
        public static void AddNewUser(Entities.User NewUser)
        {
            UsersData.AddNewUser(NewUser);
        }
        public static void DeleteUser(int UserID)
        {
            UsersData.DeleteUser(UserID);
        }

        public static void UpdateUser(int ID , UserDTO UpdatedUserDTO)
        {
            UsersData.UpdateUser(ID , UpdatedUserDTO);
        }
        public static async Task UpdateUserPasswordAsync(int UserID, string NewPassword)
        {
            UsersData.UpdateUserPasswordAsync(UserID , NewPassword);
        }


    }
}
