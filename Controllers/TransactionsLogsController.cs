using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.BL;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/TransactionsLogs")]
    [ApiController]
    public class TransactionsLogsController : ControllerBase
    {

        [HttpGet("GetAllTransactionsLogs" , Name = "GetAllTransactionsLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransactionLog>>> GetAllTransactionsLogs()
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (transactionsLogs.Count == 0 || transactionsLogs == null)
                return NotFound("Not Found Transactions Logs !");

            return Ok(transactionsLogs);

        }

        [HttpGet("{TransactionLogID}/GetTransactionLogByID", Name = "GetTransactionLogByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByID(int TransactionLogID)
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();
            
            if(!transactionsLogs.Any(t => t.id == TransactionLogID))
               return NotFound($"Not Found Transactions Logs With ID {TransactionLogID} !");

            return Ok(transactionsLogs.Where(t => t.id == TransactionLogID));

        }

        [HttpGet("GetTransactionLogByOldest", Name = "GetTransactionLogByOldest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByOldest()
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (transactionsLogs.Count == 0)
                return NotFound($"Not Found Transactions Logs !");

            return Ok(transactionsLogs.OrderBy(t => t.created_at ));

        }

        [HttpGet("GetTransactionLogByNewest", Name = "GetTransactionLogByNewest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByNewest()
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (transactionsLogs.Count == 0)
                return NotFound($"Not Found Transactions Logs !");

            return Ok(transactionsLogs.OrderByDescending(t => t.created_at));

        }

        [HttpGet("{TransactionStatus}/GetTransactionLogByStatus", Name = "GetTransactionLogByStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByStatus(string TransactionStatus)
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (!transactionsLogs.Any(t => t.status == TransactionStatus))
                return NotFound($"Not Found Transactions Logs With Status \'{TransactionStatus}\' !");

            return Ok(transactionsLogs.Where(t => t.status == TransactionStatus));

        }

        [HttpGet("{UserID}/GetTransactionLogByCreatedUser", Name = "GetTransactionLogByCreatedUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByCreatedUser(int UserID)
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (!transactionsLogs.Any(t => t.action_by == UserID))
                return NotFound($"Not Found Transactions Logs That Created From User With ID {UserID} !");

            return Ok(transactionsLogs.Where(t => t.action_by == UserID));

        }

        [HttpGet("{TransactionType}/GetTransactionLogByTransactionType", Name = "GetTransactionLogByTransactionType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionLog>> GetTransactionLogByTransactionType(string TransactionType)
        {
            List<TransactionLog> transactionsLogs = await TransactionsLogsBusinessAPI.GetAllTransactionsLog();

            if (!transactionsLogs.Any(t => t.transaction_type == TransactionType))
                return NotFound($"Not Found Transactions Logs With Transactoin Type {TransactionType} !");

            return Ok(transactionsLogs.Where(t => t.transaction_type == TransactionType));

        }




    }
}
