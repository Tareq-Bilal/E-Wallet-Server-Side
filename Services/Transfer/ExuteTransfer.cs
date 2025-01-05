using E_Wallet_Server_Side.Data;
using E_Wallet_Server_Side.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Wallet_Server_Side.Services.Transfer
{
    public static class ExuteTransfer
    {
        public static async void Excute(DTOs.Transfer.TransferDTO transferDTO)
        {
            using (var context = new AppDbContext())
            {
                //@SenderWalletID INT,
                //@ReceiverWalletID INT,
                //@Amount DECIMAL(18, 2),
                //@Description NVARCHAR(255),
                //@ActionBy INT

                var SenderWalletID   = new SqlParameter("@SenderWalletID"  , transferDTO.sender_wallet_id);
                var ReceiverWalletID = new SqlParameter("@ReceiverWalletID", transferDTO.receiver_wallet_id);
                var Amount           = new SqlParameter("@Amount"          , transferDTO.amount);
                var Description      = new SqlParameter("@Description"     , transferDTO.description);
                var ActionBy         = new SqlParameter("@ActionBy"        , transferDTO.GetActionby());// Must Be The Current User);
                await context.Database.ExecuteSqlRawAsync("EXEC SP_PerformTransfer @SenderWalletID , @ReceiverWalletID, @Amount , @Description , @ActionBy", 
                                                           SenderWalletID, ReceiverWalletID, Amount, Description , ActionBy);

                context.SaveChanges();
            }
        }
    }
}
