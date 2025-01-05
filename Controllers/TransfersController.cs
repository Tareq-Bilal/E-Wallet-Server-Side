using E_Wallet_Server_Side.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Wallet_Server_Side.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using E_Wallet_Server_Side.DTOs.Transaction;
using E_Wallet_Server_Side.DTOs.Transfer;
using E_Wallet_Server_Side.Services.Transfer;
namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/Transfers")]
    [ApiController]
    public class TransfersController : ControllerBase
    {

        [HttpGet("GetAllTransfers", Name = "GetAllTransfers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetAllTransfersAsync()
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if (transfers.Count == 0)
                return NotFound("Not Found Transfers !");

            return Ok(transfers);

        }

        [HttpGet("{TransferID}/GetTransferByID", Name = "GetTransferByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransferByID(int TransferID)
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if(!transfers.Any(tr => tr.id == TransferID))
                return NotFound($"Not Found Transfer With ID [{TransferID}] !");

            var transfer = transfers.Where(tr => tr.id == TransferID);
            return Ok(transfer);

        }

        [HttpGet("{SenderWalletID}/GetTransfersBySenderWalletID", Name = "GetTransfersBySenderWalletID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersBySenderWalletID(int SenderWalletID)
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();


            if (!transfers.Any(tr => tr.sender_wallet_id == SenderWalletID))
                return NotFound($"Not Found Transfer With Sender Wallet ID [{SenderWalletID}] !");

            var transfer = transfers.Where(tr => tr.sender_wallet_id == SenderWalletID);
            return Ok(transfer);

        }

        [HttpGet("{ReciverWalletID}/GetTransfersByReciverWalletID", Name = "GetTransfersByReciverWalletID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersByReciverWalletID(int ReciverWalletID)
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();


            if (!transfers.Any(tr => tr.receiver_wallet_id == ReciverWalletID))
                return NotFound($"Not Found Transfer With Sender Wallet ID [{ReciverWalletID}] !");

            var transfer = transfers.Where(tr => tr.receiver_wallet_id == ReciverWalletID);
            return Ok(transfer);

        }

        [HttpGet("GetTransfersByAmountDesc", Name = "GetTransfersByAmountDesc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersByAmountDesc()
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if (transfers == null)
                return NotFound($"Not Found Transfers !");

            return Ok(transfers.OrderByDescending(a => a.amount));

        }

        [HttpGet("GetTransfersByAmountAsc", Name = "GetTransfersByAmountAsc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersByAmountAsc()
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if (transfers == null)
                return NotFound($"Not Found Transfers !");

            return Ok(transfers.OrderBy(a => a.amount));

        }

        [HttpGet("GetTransfersByOldest", Name = "GetTransfersByOldest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersByOldest()
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if (transfers == null)
                return NotFound($"Not Found Transfers !");

            return Ok(transfers.OrderBy(a => a.created_at));

        }

        [HttpGet("GetTransfersByNewest", Name = "GetTransfersByNewest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Transfer>>> GetTransfersByNewest()
        {
            List<Transfer> transfers = await TransfersAPIBusiness.GetAllTransfers();

            if (transfers == null)
                return NotFound($"Not Found Transfers !");

            return Ok(transfers.OrderByDescending(a => a.created_at));

        }

        [HttpPost("AddNewTransfer", Name = "AddNewTransfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddNewTransfer([FromBody] TransferDTO transferDTO)
        {
            var Wallets = await WalletAPIBusiness.GetAllWallets();

            if (!Wallets.Any(tr => tr.ID == transferDTO.sender_wallet_id))
                return NotFound($"Not Found Sender Wallet ID [{transferDTO.sender_wallet_id}] !");

            if (!Wallets.Any(tr => tr.ID == transferDTO.receiver_wallet_id))
                return NotFound($"Not Found Receiver Wallet ID [{transferDTO.receiver_wallet_id}] !");

            if (!await Vaildation.TransferAmountValidation.IsValidTransferAmountAsync(transferDTO.sender_wallet_id, transferDTO.receiver_wallet_id))
                return BadRequest($"Sender Wallet With ID [{transferDTO.sender_wallet_id}] Has Not Enough Balance To Transfer {transferDTO.amount} $ !");

            transferDTO.SetActionby(1); // Must Be The Current User

            Services.Transfer.ExuteTransfer.Excute(transferDTO);

            return Ok(transferDTO);

        }
    }
}
