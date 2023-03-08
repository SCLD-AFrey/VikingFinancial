using System.Globalization;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TransactionService.Services.Database;
using ValkyrieFinancial.Protos;
using VikingFinancial.Data.Transaction;

namespace TransactionService.Services
{
    public class TransactionService : TransactionsService.TransactionsServiceBase
    {
        private readonly ILogger<TransactionService> m_logger;
        private readonly TransactionDatabaseInterface m_dbInterface;
        public TransactionService(ILogger<TransactionService> p_logger,
            TransactionDatabaseInterface p_dbInterface)
        {
            m_logger = p_logger;
            m_dbInterface = p_dbInterface;
        }

        public override async Task<GetTransactionRangeResponse> GetTransactionRange(GetTransactionRangeRequest p_request, ServerCallContext p_context)
        {
            var response = new GetTransactionRangeResponse();
            try
            {
                response.TransactionRange = new G_TransactionRange();
                
                XPCollection<Transaction> transactions = new(m_dbInterface.ProvisionUnitOfWork());
                transactions.Criteria = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator(nameof(Transaction.DateTransaction), p_request.StartDate.ToDateTime(), BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator(nameof(Transaction.DateTransaction), p_request.EndDate.ToDateTime(), BinaryOperatorType.LessOrEqual));
                foreach (var t in transactions)
                {
                    
                }


            } catch (Exception e)
            {
                response.Error = new G_Error()
                {
                    Message = "Error getting transaction range : " + e.Message,
                };
                m_logger.LogError(e, "Error getting transaction range");
            }




            return await Task.FromResult(response);
        }
    }
}