using System.ComponentModel.DataAnnotations;

namespace E_Wallet_Server_Side.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public DateTime created_at { get;  set; }



    }
}
