using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.DTOs;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet_Server_Side.DAL
{
    public class UsersData
    {

        public static List<Entities.User> GetAllUsers()
        {
            List<Entities.User> users = new List<Entities.User>();

            using (var context = new AppDbContext())
            {
                foreach (var entityUser in context.Users)
                {
                    users.Add(new Entities.User
                    {
                        ID = entityUser.ID, // Adjusted to match the property name in entityUser
                        userName = entityUser.userName,
                        email = entityUser.email,
                        created_at = entityUser.created_at,
                    });
                }
            }
            return users;
        }
        public static Entities.User GetUserByID(int ID)
        {
            Entities.User user = new Entities.User();

            using (var context = new AppDbContext())
            {
                return context.Users.SingleOrDefault(u => u.ID == ID );
            }
        }
        public static Entities.User GetUserByName(string Name)
        {
            Entities.User user = new Entities.User();

            using (var context = new AppDbContext())
            {
                return context.Users.SingleOrDefault(u => u.userName == Name);
            }
        }
        public static bool IsUserExist(int UserID)
        {
            using (var context = new AppDbContext())
            {
                return context.Users.Any(u => u.ID == UserID);
            }

        }
        public static bool IsUserExist(string UserName)
        {
            using (var context = new AppDbContext())
            {
                return context.Users.Any(u => u.userName == UserName);
            }

        }
        public static void AddNewUser(Entities.User NewUser)
        {
            using (var context = new AppDbContext())
            {

                context.Add(NewUser);
                context.SaveChanges();

            }
        }
        public static void DeleteUser(int UserID)
        { 
            using (var context = new AppDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.ID == UserID);

                context.Remove(user);  
                context.SaveChanges();
            
            }

        }
        public static void UpdateUser(int ID , UserDTO UpdatedUser)
        {
            using (var context = new AppDbContext())
            {
                var OldUser = context.Users.SingleOrDefault(u => u.ID == ID);

                if (OldUser == null)
                    throw new Exception($"User With ID {UpdatedUser.ID} Deos Not Found !!"); 
                

                OldUser.email = UpdatedUser.email;
                OldUser.userName = UpdatedUser.userName;
             //   OldUser.SetPassword(PasswordHashing.ComputeHash(UpdatedUser.password_hash));
                context.SaveChanges();

            }
        }
        public static async Task UpdateUserPasswordAsync(int UserID , string NewPassword)
        {
            using (var context = new AppDbContext())
            {
                var userIdParam = new SqlParameter("@UserID", UserID);
                var newPassword = new SqlParameter("@NewPassword", PasswordHashing.ComputeHash(NewPassword));
                
                await context.Database.ExecuteSqlRawAsync("EXEC SP_UpdateUserPassword @UserID ,@NewPassword ", userIdParam , newPassword);

                context.SaveChanges();
            }
        }


    }
}
