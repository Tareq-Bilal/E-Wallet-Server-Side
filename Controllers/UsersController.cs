using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.DTOs;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
     

        [HttpGet("GetAllUsers" , Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <IEnumerable<Entities.User>> GetAllUsers()
        {
                List <Entities.User> users = new List<Entities.User>();

                users = BL.UserAPIBusiness.GetAllUsers();

                if(users.Count == 0 || users == null)
                    return NotFound("Users Not Found !!");

                else
                    return Ok(users);

        }

        [HttpGet("{UserID}/GeUserByID", Name = "GeUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult <Entities.User> GeUserByID(int UserID)
        {

            if (UserID < 1)
                return BadRequest("User ID Should Be Postive !");

            Entities.User user = UserAPIBusiness.GetUserByID(UserID);

            if(user == null)
                return NotFound($"No User Found With ID [{UserID}]!");

            return Ok(user);
        }

        [HttpGet("{UserName}/GeUserByName", Name = "GeUserByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Entities.User> GeUserByName(string UserName)
        {

            if (string.IsNullOrEmpty(UserName))
                return BadRequest("Please Enter The User Name !");

            Entities.User user = UserAPIBusiness.GetUserByName(UserName.Trim());

            if (user == null)
                return NotFound($"No User Found With Name \"{UserName}\" !");

            return Ok(user);
        }

        [HttpGet("{UserID}/IsUserExist", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsUserExist(int UserID)
        {

            if (UserID < 1)
                return BadRequest("Please Enter Positive User ID !");

            if (UserAPIBusiness.IsUserExist(UserID))
                return Ok($"User With ID {UserID} is Exist !");

            return NotFound($"Not Found User With ID {UserID}");

        }


        [HttpPost("AddNewUser", Name = "AddNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewUser([FromBody] DTOs.UserDTO NewUserDTO)
        {
            if (NewUserDTO  == null && !Vaildation.AddNewEntityrInputValidation.IsValidInput(NewUserDTO))
                return BadRequest("Please Enter User Info Correctly");

            Entities.User user = new Entities.User
            {

                userName = NewUserDTO.userName,
                email = NewUserDTO.email,
                created_at = DateTime.UtcNow
            };

            user.SetPassword(PasswordHashing.ComputeHash(NewUserDTO.password_hash));

            UserAPIBusiness.AddNewUser(user);

            NewUserDTO.ID = user.ID;

            return CreatedAtRoute("GeUserByID", new { UserID = NewUserDTO.ID }, user);

        }

        [HttpPut("{ID}/UpdateUser", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(int ID, [FromBody] UserDTO UpdatedUserDTO)
        {
            if (ID < 1)
                return BadRequest("Please Enter Positive User ID !");

            if (!UserAPIBusiness.IsUserExist(ID))
                return NotFound($"Not Found User With ID [{ID}] !");

            if (UpdatedUserDTO == null || !Vaildation.UpdateEntityInpuVaildation.IsValidInput(UpdatedUserDTO))
                return BadRequest("Invaild User Info !");


            UserAPIBusiness.UpdateUser(ID, UpdatedUserDTO);
            return Ok($"User With ID [{ID}] Updated Successfully ! , (Password Will Not Change)");

        }

        [HttpPut("{ID}/{OldPassword}/{NewPassword}/UpdateUserPassword", Name = "UpdateUserPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserPassword(int ID , string OldPassword , string NewPassword)
        {
            if (ID < 1)
                return BadRequest("Please Enter Positive User ID !");

            if (!UserAPIBusiness.IsUserExist(ID))
                return NotFound($"Not Found User With ID [{ID}] !");

            // Creeate Servoice If The Entered Old Password Match The Real Old Password
            var user = UserAPIBusiness.GetUserByID(ID);

            if (user.GetPassword() != PasswordHashing.ComputeHash(OldPassword))
                return BadRequest("You Password Input Does Not Match With The Current One ! , Enter The Correct Password");

            UserAPIBusiness.UpdateUserPasswordAsync(ID, NewPassword);
            return Ok($"User \"{user.userName}\" Updated His Password Successfully"); 

        }

        [HttpDelete("{UserID}/DeleteUser", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int UserID)
        {
            if (UserID < 1)
                return BadRequest("Please Enter Positive User ID !");

            if (!UserAPIBusiness.IsUserExist(UserID))
                return NotFound($"Not Found User With ID [{UserID}] !");

            UserAPIBusiness.DeleteUser(UserID);

            return (!UserAPIBusiness.IsUserExist(UserID) ?
                Ok($"User With ID [{UserID}] Deleted Successflluy" ):
                BadRequest("Deletion Failed !!"));


        }

       
    }
}
