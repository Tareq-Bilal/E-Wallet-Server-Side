using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.DAL;
using E_Wallet_Server_Side.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/TransferLogs")]
    [ApiController]
    public class TransferLogsController : ControllerBase
    {
        [HttpGet("GetAllTransfersLogs", Name = "GetAllTransfersLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransferLog>>> GetAllTransfersLogsAsync()
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (transfersLogs.Count == 0)
                return NotFound("Not Found Transfers Logs!");

            return Ok(transfersLogs);

        }

        [HttpGet("{TransferLogID}/GetTransferLogByID", Name = "GetTransferLogByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransferLog>> GetTransferLogByID(int TransferLogID)
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (!transfersLogs.Any(tr => tr.id == TransferLogID))
                return NotFound($"Not Found Transfer Log With ID [{TransferLogID}] !");

            var transferLog = transfersLogs.Where(tr => tr.id == TransferLogID);
            return Ok(transferLog);

        }

        [HttpGet("{TransferID}/GetTransferLogByTransferID", Name = "GetTransferLogByTransferID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransferLog>> GetTransferLogByTransferID(int TransferID)
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (!transfersLogs.Any(tr => tr.transfer_id == TransferID))
                return NotFound($"Not Found Transfer Log With Transfer ID [{TransferID}] !");

            var transferLog = transfersLogs.Where(tr => tr.transfer_id == TransferID);
            return Ok(transferLog);

        }

        [HttpGet("GetSuccessfulTransferLogs", Name = "GetSuccessfulTransferLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransferLog>>> GetSuccessfulTransferLogs()
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (!transfersLogs.Any(tr => tr.status == "Successful"))
                return NotFound($"Not Found Successful Transfer Logs !");

            var SuccessfulTransferLogs = transfersLogs.Where(tr => tr.status == "Successful");
            return Ok(SuccessfulTransferLogs);

        }

        [HttpGet("GetFailedTransferLogs", Name = "GetFailedTransferLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransferLog>>> GetFailedTransferLogs()
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (!transfersLogs.Any(tr => tr.status == "Failed"))
                return NotFound($"Not Found Failed Transfer Logs !");

            var FailedTransferLogs = transfersLogs.Where(tr => tr.status == "Failed");
            return Ok(FailedTransferLogs);

        }

        [HttpGet("GetTransferLogsByOldest", Name = "GetTransferLogsByOldest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransferLog>>> GetTransferLogsByOldest()
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            return Ok(transfersLogs.OrderBy(trl => trl.created_at));

        }

        [HttpGet("GetTransferLogsByNewest", Name = "GetTransferLogsByNewest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransferLog>>> GetTransferLogsByNewest()
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            return Ok(transfersLogs.OrderByDescending(trl => trl.created_at));

        }

        [HttpGet("{ActionByID}/GetTransferLogByActionByID", Name = "GetTransferLogByActionByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransferLog>> GetTransferLogByActionByID(int ActionByID)
        {
            var transfersLogs = await TransfersLogsAPIBusiness.GetTransfersLogsAsync();

            if (!transfersLogs.Any(tr => tr.action_by == ActionByID))
                return NotFound($"Not Found Transfer Log With Action By ID [{ActionByID}] !");

            var transferLog = transfersLogs.Where(tr => tr.action_by == ActionByID);
            return Ok(transferLog);

        }

    }
}
