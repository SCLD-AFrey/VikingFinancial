using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using valkyriefinancial.protos.transactions;
using VikingFinancial.Data.Transaction;
using VikingFinancial.Protos;
using VikingFinancial.WebController;

namespace VikingFinancial.WebController.Services
{
    public class TransactionService : TransactionsService.TransactionsServiceBase
    {
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(ILogger<TransactionService> p_logger)
        {
            _logger = p_logger;
        }

        public override Task<G_ServiceCheckResponse> GetServiceCheck(G_ServiceCheckRequest p_request, ServerCallContext p_context)
        {
            try
            {
                
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
                _logger.LogError(e, "Error in GetServiceCheck - {EMessage}", e.Message);
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

        public override Task<G_GetTransactionResponse> GetTransactions(G_GetTransactionRequest request, ServerCallContext context)
        {
            var response = new G_GetTransactionResponse();
            //TODO get transactions from database
            return Task.FromResult(response);
        }

        public override Task<G_AddTransactionResponse> AddTransaction(G_AddTransactionRequest p_request, ServerCallContext p_context)
        {
            var response = new G_AddTransactionResponse();

            try
            {
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