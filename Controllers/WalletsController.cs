using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Vaildation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/Wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {

        [HttpGet("GetAllWallets", Name = "GetAllWallets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<WalletDTO>>> GetAllWallets()
        {

            List<WalletDTO> Wallets = await WalletAPIBusiness.GetAllWallets();

            if(Wallets.Count == 0 || Wallets == null)
                return NotFound("No Wallets Exists !!");

            return Ok(Wallets);


        }

        [HttpGet("{UserName}/GetWalletByUserName", Name = "GetWalletByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<WalletDTO> GetWalletByUserName(string UserName)
        {

            if (string.IsNullOrEmpty(UserName))
                return BadRequest("User Name Is Required !");


            if (!UserAPIBusiness.IsUserExist(UserName.Trim()))
                return NotFound($"No User Found With Name \"{UserName}\" !");

            WalletDTO wallet = WalletAPIBusiness.GetAllWalletByUserName(UserName);
            
            if(wallet == null)
                return NotFound($"No Wallet Found With User Name \"{UserName}\" !");

            return Ok(wallet);

        }


        [HttpGet("{WalletID}/GetWalletByID", Name = "GetWalletByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WalletDTO> GetWalletByID(int WalletID)
        {
            if (WalletID < 1)
                return BadRequest("Enter Positive Wallet ID !");

            WalletDTO wallet = WalletAPIBusiness.GetAllWalletByID(WalletID);

            if (wallet == null)
                return NotFound($"No Wallet Found With ID [{WalletID}]");

            return Ok(wallet);

        }

        [HttpGet("{Currency}/GetWalletsByCurrency", Name = "GetWalletsByCurrency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<WalletDTO>>> GetWalletsByCurrency(string Currency)
        {
            var walltes = await WalletAPIBusiness.GetAllWallets();

            if (!walltes.Any(c => c.currency == Currency.ToUpper()))
                return NotFound($"Currency {Currency} Does Not Exist !");


            var result = walltes.Filter(w => w.currency == Currency.ToUpper());

            if (result == null)
                return NotFound($"Not Found Wallets With Currency {Currency} !");

            else
                return Ok(result);

        }

        [HttpPost("AddNewWallet" , Name = "AddNewWallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddNewWallet([FromBody] AddWalletDTO NewWallet)
        {
            if (NewWallet.user_id < 0)
                return BadRequest("Please Enter Positive User ID !");

            if (!UserAPIBusiness.IsUserExist(NewWallet.user_id))
                return NotFound($"Not Found User With ID [{NewWallet.user_id}] !");

            if(NewWallet.balance < 0)
                return BadRequest("Please Enter Positive Balance !");

            if (string.IsNullOrEmpty(NewWallet.currency))
                return BadRequest("Please Enter Your Currency !");


            Wallet wallet = new Wallet
            {

                user_id = NewWallet.user_id,
                currency = NewWallet.currency.ToUpper(),
                created_at = DateTime.Now

            };

            wallet.SetBalance(NewWallet.balance);

            WalletAPIBusiness.AddNewWallet(wallet);

            NewWallet.ID = wallet.ID;

            return CreatedAtRoute("GetWalletByID", new { WalletID = NewWallet.ID }, wallet);


        }

        [HttpPut("UpdateWallet", Name = "UpdateWallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateUser([FromBody] UpdateWalletDTO updateWalletDTO)
        {
            if(updateWalletDTO.ID < 0)
                return BadRequest("Please Positive Wallet ID !");

            if(!WalletAPIBusiness.IsWalletExist(updateWalletDTO.ID))
                return NotFound($"Not Found Wallet With ID [{updateWalletDTO.ID}] !");

            if (updateWalletDTO.user_id < 0)
                return BadRequest("Please Positive User ID !");

            if (!UserAPIBusiness.IsUserExist(updateWalletDTO.user_id))
                return NotFound($"Not Found User With [{updateWalletDTO.user_id}] !");

            if (updateWalletDTO.balance < 0)
                return BadRequest("Please Positive Balance !");

            if (string.IsNullOrEmpty(updateWalletDTO.currency))
                return BadRequest("Please Enter The Wallet Currency !");


            WalletAPIBusiness.UpdateWallet(updateWalletDTO);

            return CreatedAtRoute("GetWalletByID",
                new { WalletID = updateWalletDTO.ID },
                new { Message = $"Wallet with ID {updateWalletDTO.ID} Updated Successfully!", Data = updateWalletDTO });

        }

        [HttpDelete("{WalletID}/DeleteWallet" , Name = "DeleteWallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteWallet(int WalletID)
        {
            if(WalletID < 1)
                return BadRequest("Enter Positive Wallet ID !");


            if (!WalletAPIBusiness.IsWalletExist(WalletID))
                return NotFound($"No Wallet Found With ID [{WalletID}]");

            WalletDTO wallet = WalletAPIBusiness.GetAllWalletByID(WalletID);
            WalletAPIBusiness.DeleteWallet(WalletID);

            if (!WalletAPIBusiness.IsWalletExist(WalletID))
                return Ok($"Wallet With ID [{WalletID}] Deleted Successfully");


            return BadRequest("Deletion Failed !!");

        }



    }
}
