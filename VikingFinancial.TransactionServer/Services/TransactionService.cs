using System.Globalization;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos.Transactions;
using VikingFinancial.Data.Transaction;
using VikingFinancial.TransactionServer.Services.Database;

namespace VikingFinancial.TransactionServer.Services
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

        public override Task<G_DetailsResponse> GetDetails(G_DetailsRequest p_request, ServerCallContext p_context)
        {
            var response = new G_DetailsResponse();

            try
            {
                if (!DateTime.TryParse(p_request.StartDate.ToDateTime().ToString(CultureInfo.InvariantCulture),
                        out var startDate))
                    startDate = DateTime.Parse($"{DateTime.Today.Year}-{DateTime.Today.Month}-01");
                
                if (!DateTime.TryParse(p_request.StartDate.ToDateTime().ToString(CultureInfo.InvariantCulture),
                        out var endDate))
                    endDate = DateTime.Parse($"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)}");


                using var unitOfWork = m_dbInterface.ProvisionUnitOfWork();
                XPCollection<Transaction> transactions = new XPCollection<Transaction>(unitOfWork);
                transactions.Criteria = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator("Date", startDate, BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator("Date", endDate, BinaryOperatorType.LessOrEqual));


                response.Success = new G_DetailsSuccess()
                {
                    Balance = 0
                };
                double bal = transactions.Sum(p_transaction => p_transaction.Amount);
            }
            catch (Exception e)
            {
                m_logger.LogError(e, "Error in GetDetails - {EMessage}", e.Message);
                response.Failure = new G_DetailsFailure()
                {
                    Reason = e.Message
                };
            }
            
            
            return Task.FromResult(new G_DetailsResponse());
        }

        public override Task<G_ServiceCheckResponse> GetServiceCheck(G_ServiceCheckRequest p_request, ServerCallContext p_context)
        {
            try
            {
                using var unitOfWork = m_dbInterface.ProvisionUnitOfWork();
                return Task.FromResult(new G_ServiceCheckResponse()
                {
                    CheckTime = Timestamp.FromDateTime(DateTime.UtcNow),
                    Success = new G_ServiceCheckResponseSuccess()
                    {
                        Result = ""
                    }
                });
            } catch (Exception e)
            {
                m_logger.LogError(e, "Error in GetServiceCheck - {EMessage}", e.Message);
                return Task.FromResult(new G_ServiceCheckResponse()
                {
                    CheckTime = Timestamp.FromDateTime(DateTime.UtcNow),
                    Failure = new G_ServiceCheckResponseFailure()
                    {
                        Reason = e.Message
                    }
                });
            }
        }

        public override Task<G_GetTransactionResponse> GetTransactions(G_GetTransactionRequest p_request, ServerCallContext p_context)
        {
                using var unitOfWork = m_dbInterface.ProvisionUnitOfWork();
                var response = new G_GetTransactionResponse();

                using (var transactions = new XPCollection<Transaction>(unitOfWork))
                {
                    foreach (var trans in transactions)
                    {
                        response.Transactions.Add(new G_Transaction()
                        {
                            Oid = trans.Oid,
                            AddedByUser = trans.AddedByUser.Oid,
                            Amount = trans.Amount,
                            DateCreated = Timestamp.FromDateTime(trans.DateCreated),
                            DateTransaction = Timestamp.FromDateTime(trans.DateTransaction)
                        });
                    }
                }

                //TODO get transactions from database
                return Task.FromResult(response);
        }

        public override Task<G_AddTransactionResponse> AddTransaction(G_AddTransactionRequest p_request, ServerCallContext p_context)
        {
            var response = new G_AddTransactionResponse();

            try
            {
                using var unitOfWork = m_dbInterface.ProvisionUnitOfWork();
                switch (p_request.Transaction.RequestCase)
                {
                    case G_Transaction.RequestOneofCase.None:
                        response.Failure = new G_TransactionFailureResponse(){ Reason = "Transaction Request Case not found"};
                        break;
                    case G_Transaction.RequestOneofCase.Credit:
                        //TODO add credit transactions to database
                        response.Success = new G_TransactionSuccessResponse
                        {
                            Transaction = p_request.Transaction
                        };
                        break;
                    case G_Transaction.RequestOneofCase.Debit:
                        if (p_request.Transaction.Amount > 0)
                        {
                            p_request.Transaction.Amount *= -1;
                        }
                        //TODO add debit transactions to database
                        response.Success = new G_TransactionSuccessResponse
                        {
                            Transaction = p_request.Transaction
                        };
                        break;
                    default:
                        response.Failure = new G_TransactionFailureResponse(){ Reason = ""};
                        break;
                }
                return Task.FromResult(response);
            } catch (Exception e)
            {
                response.Failure = new G_TransactionFailureResponse(){ Reason = e.Message};
                return Task.FromResult(response);
            }
        }
    }
}