using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.DTOs.PaymentMethod;
using E_Wallet_Server_Side.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/UsersPaymentMethods")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        [HttpGet("GetAllUsersPaymentMethods" , Name = "GetAllUsersPaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PaymentMethod>>> GetAllUsersPaymentMethods()
        {

            var PaymentMethods = await PaymentMethodAPIBusiness.GetPaymentMethodsAsync();

            if(PaymentMethods.Count == 0)
                return NotFound("No Payment Methods Found !");

            return Ok(PaymentMethods);


        }

        [HttpGet("{UserID}/GetUserPaymentMethods", Name = "GetUserPaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PaymentMethod>>> GetUserPaymentMethods(int UserID)
        {

            var PaymentMethods = await PaymentMethodAPIBusiness.GetPaymentMethodsAsync();

            if (!PaymentMethods.Any(m => m.user_id == UserID))
                return NotFound($"No Payment Methods Found for User With ID {UserID}!");

            return Ok(PaymentMethods.Where(m => m.user_id == UserID));


        }

        [HttpGet("GetPaymentMethodsSortedByUserId", Name = "GetPaymentMethodsSortedByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethodsSortedByUserId()
        {

            var PaymentMethods = await PaymentMethodAPIBusiness.GetPaymentMethodsAsync();

            if (PaymentMethods.Count == 0)
                return NotFound($"No Payment Methods Found !");

            return Ok(PaymentMethods.OrderBy(m => m.user_id));


        }


        [HttpPost("AddUserPaymnetMethod", Name = "AddUserPaymnetMethod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethod>> AddUserPaymnetMethod([FromBody] PaymentMethodDTO paymentMethodDTO)
        {
            var User = UserAPIBusiness.GetUserByID(paymentMethodDTO.user_id);

            if (User == null)
                NotFound($"Not Found User With ID [{paymentMethodDTO.user_id}] !");

            if (string.IsNullOrEmpty(paymentMethodDTO.method_type))
                return BadRequest("Please Enter The Payment Method Type !");

            if(string.IsNullOrEmpty(paymentMethodDTO.details))
                return BadRequest("Please Enter The Payment Method Details !");


            PaymentMethod paymentMethod = new PaymentMethod(paymentMethodDTO);

            PaymentMethodAPIBusiness.AddUserPaymentMethod(paymentMethod);


            return CreatedAtRoute("GetWalletByID", new { WalletID = paymentMethod.id }, paymentMethod);

        }

        
        [HttpPut("{id}/UpdatePaymentMethod" , Name = "UpdatePaymentMethod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePaymentMethod(int id, PaymentMethodUpdateDto updateDto)
        {
            var PaymentMethods = await PaymentMethodAPIBusiness.GetPaymentMethodsAsync();

            if (!PaymentMethods.Any(pm => pm.id == id))
                return NotFound($"Not Found User Payment Method With ID {id} !");
               
            if(!PaymentMethods.Any(pm => pm.user_id == updateDto.user_id))
                return NotFound($"Not Found Payment Method For User {updateDto.user_id} !");

            if (string.IsNullOrEmpty(updateDto.method_type))
                return BadRequest("Please Enter The Payment Method Type !");

            if (string.IsNullOrEmpty(updateDto.details))
                return BadRequest("Please Enter The Payment Method Details !");


            if (await PaymentMethodAPIBusiness.UpdatePaymentMethod(id, updateDto))
                return Ok($"User [{updateDto.user_id}] Payment Method Updated Successfully ✔");

            else
                return BadRequest("Payment Method Updating Failed !");


        }

        //Delete Payment Method
        [HttpDelete("{id}/{user_id}/DeleteUserPaymentMethod", Name = "DeleteUserPaymentMethod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserPaymentMethod(int id , int user_id)
        {
            var PaymentMethods = await PaymentMethodAPIBusiness.GetPaymentMethodsAsync();

            if (!PaymentMethods.Any(pm => pm.id == id && pm.user_id == user_id))
                return NotFound($"Not Found User Payment Method With ID {id} and User Id {user_id} !");

            if (await PaymentMethodAPIBusiness.DeleteUserPaymentMethod(id))
                return NotFound($"Payment Method With ID {id} Deleted Successfuly ✔");

            else
                return NotFound($"Deleted Failed ✘");


        }

    }
}
