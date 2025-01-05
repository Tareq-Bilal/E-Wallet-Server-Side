using E_Wallet_Server_Side.BL;
using E_Wallet_Server_Side.DTOs.Transaction;
using E_Wallet_Server_Side.DTOs.Wallet;
using E_Wallet_Server_Side.Entities;
using E_Wallet_Server_Side.Services.Transactoin_Operations;
using E_Wallet_Server_Side.Services;
using E_Wallet_Server_Side.Vaildation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Wallet_Server_Side.Controllers
{
    [Route("api/Transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        [HttpGet("GetAllTransactions", Name = "GetAllTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetAllTransactions()
        {

            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();

            if (Transactions.Count == 0 || Transactions == null)
                return NotFound("No Transactions Exists !!");

            return Ok(Transactions);


        }


        [HttpGet("{TransactionID}/GetTransactionsByID", Name = "GetTransactionsByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetTransactionsByID(int TransactionID)
        {
            if (TransactionID < 0)
                return BadRequest("Please Enter Positive Transaction ID !");

            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();

            if(!Transactions.Any(t => t.transaction_id == TransactionID))
                return NotFound($"Not Found Transaction With ID {TransactionID} !");

            var Result = Transactions.TransactionFilter(t => t.transaction_id == TransactionID);

            if (Result == null)
                return NotFound($"No Transactions Found With ID {TransactionID} !");

            return Ok(Result);


        }

        [HttpGet("GetTransactionsByAmountDesc", Name = "GetTransactionsByAmountDesc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetTransactionsByAmountDesc()
        {
            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();
            var Result = Transactions.OrderByDescending(t => t.amount);

            if (Result == null)
                return NotFound($"No Transactions Exist !");

            return Ok(Result);


        }


        [HttpGet("GetTransactionsByAmountAesc", Name = "GetTransactionsByAmountAesc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetTransactionsByAmountAesc()
        {
            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();
            var Result = Transactions.OrderBy(t => t.amount);

            if (Result == null)
                return NotFound($"No Transactions Exist !");

            return Ok(Result);


        }

        [HttpGet("{From}/{To}/GetTransactionsBetweenTwoDates", Name = "GetTransactionsBetweenTwoDates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetTransactionsBetweenTwoDates(string From, string To)
        {
            if (!Vaildation.ValidateTransactionFromAndToDatesInput.ValidateDateFormats(From, out DateTime dFrom))
                return BadRequest($"Invalid 'From' date format. Allowed formats: yyyy-MM-dd, dd/MM/yyyy, MM/dd/yyyy, etc.");

            if (!Vaildation.ValidateTransactionFromAndToDatesInput.ValidateDateFormats(To, out DateTime dTo))
                return BadRequest($"Invalid 'To' date format. Allowed formats: yyyy-MM-dd, dd/MM/yyyy, MM/dd/yyyy, etc.");

            if (dFrom.CompareTo(dTo) > 0)
                return BadRequest("The 'From' date should be earlier than the 'To' date.");

            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();
            var Result = Transactions.Where(t => t.transaction_date >= dFrom && t.transaction_date <= dTo.AddDays(1));

            if (!Result.Any())
                return NotFound($"No transactions exist between {From} and {To}!");

            return Ok(Result);
        }

       
        [HttpGet("{TransactoinType}/GetTransactionsByTransactoinType", Name = "GetTransactionsByTransactoinType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetTransactionsByTransactoinType(string TransactoinType)
        {

            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();
            List<E_Wallet_Server_Side.Entities.TransactionTypes> TransactionTypes = await TransactoinTypesAPIBusiness.GetAllTransactionTypes();

            if (!TransactionTypes.Any(t => t.name == TransactoinType))
                return BadRequest($"Please Enter Correct Transaction Type  !");

            var Result = Transactions.Where(t => t.transaction_type == TransactoinType);

            if (Result == null)
                return NotFound($"No Transactions Exist Found With Type \"{TransactoinType}\" !");

            return Ok(Result);


        }

        [HttpGet("{WalletID}/GetWalletTransactions", Name = "GetWalletTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<E_Wallet_Server_Side.Entities.Transaction>>> GetWalletTransactions(int WalletID)
        {
            if (!WalletAPIBusiness.IsWalletExist(WalletID))
                return NotFound($"No Wallet Found With ID [{WalletID}] !");

            List<Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();

            if(!Transactions.Any(w => w.wallet_id == WalletID))
                return NotFound($"Wallet With ID [{WalletID}] Has Not Any Transactoins !");

            List<E_Wallet_Server_Side.Entities.Transaction> WalletTransactions = new List<Entities.Transaction>();
            
            WalletTransactions = Transactions.Where(t => t.wallet_id == WalletID).ToList();

            return Ok(WalletTransactions);

        }

        [HttpPost("AddNewTransaction", Name = "AddNewTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddNewTransaction([FromBody] TransactionDTO NewTransactoin)
        {


            if (NewTransactoin.wallet_id < 0)
                return BadRequest("Please Enter Positive Wallet ID !");

            if (!WalletAPIBusiness.IsWalletExist(NewTransactoin.wallet_id))
                return NotFound($"Not Found Wallet With ID [{NewTransactoin.wallet_id}] !");

            if (NewTransactoin.amount < 0)
                return BadRequest("Please Enter Positive Amount !");

            List<E_Wallet_Server_Side.Entities.TransactionTypes> TransactionTypes = await TransactoinTypesAPIBusiness.GetAllTransactionTypes();

            if (!TransactionTypes.Any(type => type.name == NewTransactoin.transaction_type))
                return NotFound($"Not Found Transactoin Type \"{NewTransactoin.transaction_type}\" !");

            if(NewTransactoin.status == "COMPLETED" || NewTransactoin.status == "PENDING")
            {

                if (NewTransactoin.transaction_type == "Payment" || NewTransactoin.transaction_type == "Withdraw")
                {
                    if (PaymentAndWithdrawAmountValidation.IsBalanceEnoughForTransaction(NewTransactoin.wallet_id, NewTransactoin.amount))
                    {
                        if(NewTransactoin.status == "COMPLETED")
                            ExcuteTransaction.Excute(NewTransactoin.transaction_type, NewTransactoin.wallet_id, NewTransactoin.amount);
                    }

                    else
                        return BadRequest($"Not Enough Balance int Wallet [{NewTransactoin.wallet_id}] !");

                }
                else
                    ExcuteTransaction.Excute(NewTransactoin.transaction_type, NewTransactoin.wallet_id, NewTransactoin.amount);
            }

            NewTransactoin.status = NewTransactoin.status.ToUpper();
            Entities.Transaction transaction = new Entities.Transaction(NewTransactoin);


            TransactionAPIBusiness.AddTransactoin(transaction);

            NewTransactoin.transaction_id = transaction.transaction_id;

            DTOs.TransactionLog.TransactionLogDTO transactionLog = new DTOs.TransactionLog.TransactionLogDTO{

                transaction_id = transaction.transaction_id,
                transaction_type = transaction.transaction_type,
                status = NewTransactoin.status,
                action_by = 1,// Should Be Current User ID 
                message = Services.TransactionsMessageGeneration.
                          TransactionMessageGenerator.
                          GenerateTransactionMessage(transaction.status, transaction.transaction_type, transaction.amount, transaction.wallet_id),
                created_at = transaction.transaction_date 

            };

            Services.Transactions_Logging.TransactionLoggingService.LogTransaction(transactionLog); //--> Logging Transaction

            return  CreatedAtRoute("GetTransactionsByID", new { TransactionID = NewTransactoin.transaction_id},
                                    new { Message = $"{transactionLog.message}", Data = transaction });

        }

        [HttpPut("UpdateTransactionStatus", Name = "UpdateTransactionStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTransactionStatus([FromBody] UpdateTransactionDTO NewTransactoinStatus)
        {

            if (NewTransactoinStatus.TransactionId < 0)
                return BadRequest("Please Enter Positive Transaction ID !");

            List<E_Wallet_Server_Side.Entities.Transaction> Transactions = await TransactionAPIBusiness.GetAllTransactions();

            if(!Transactions.Any(t => t.transaction_id == NewTransactoinStatus.TransactionId))
                return NotFound($"Not Found Transaction With ID {NewTransactoinStatus.TransactionId}");

            if (!VaildTransactionStatuses.ValidStatuses.Contains(NewTransactoinStatus.NewStatus.ToUpper()))
                return NotFound($"Not Found Transaction Status \"{NewTransactoinStatus.NewStatus}\" !");

            var transaction = Transactions.SingleOrDefault(t => t.transaction_id == NewTransactoinStatus.TransactionId);
            string OldTransactionStatus = transaction.status.ToUpper();

            if (!Vaildation.ChangeTransactionStatusValidation.IsVaildStatusChange(transaction.status, NewTransactoinStatus.NewStatus.ToUpper()))
                return BadRequest($"Update Transaction {transaction.transaction_id} Status From {OldTransactionStatus} To {NewTransactoinStatus.NewStatus.ToUpper()} Is Not Valid !");

            Services.UpdateTransactionStatus.UpdateStatus(NewTransactoinStatus.TransactionId, NewTransactoinStatus.NewStatus.ToUpper());
           
            DTOs.TransactionLog.TransactionLogDTO transactionLog = new DTOs.TransactionLog.TransactionLogDTO
            {

                transaction_id = transaction.transaction_id,
                transaction_type = transaction.transaction_type,
                status = NewTransactoinStatus.NewStatus.ToUpper()   ,
                action_by = 1,// Should Be Current User ID 
                message = $"Transaction {NewTransactoinStatus.TransactionId} OF Type \'{transaction.transaction_type}\' Status Updated From \'{OldTransactionStatus}\' To \'{NewTransactoinStatus.NewStatus.ToUpper()}\' Successfully",
                created_at = DateTime.Now,

            };

            NewTransactoinStatus.SetIsSuccessfulSatatus(true);
            NewTransactoinStatus.SetMessage(transactionLog.message);

            //Log The Update Transaction Status
            Services.Transactions_Logging.TransactionLoggingService.LogTransaction(transactionLog); 

            return Ok(NewTransactoinStatus);

        }


    }
}
