using System.Globalization;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ValkyrieFinancial.Protos;
using VikingFinancial.Data.Transaction;

namespace TransactionService.Services;

public class ConnectivityService : Connectivity.ConnectivityBase
{
    private readonly ILogger<ConnectivityService> m_logger;

    public ConnectivityService(ILogger<ConnectivityService> p_logger)
    {
        m_logger = p_logger;
    }

    public override Task<G_ConnectCheckResponse> CheckServerConnection(Empty p_request, ServerCallContext p_context)
    {
        return Task.FromResult(new G_ConnectCheckResponse()
        {
            Success = new G_ConnectCheckSuccess()
            {
                Message = "Success",
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            }
        });
    }
}