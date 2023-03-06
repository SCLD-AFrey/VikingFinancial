using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models.Interceptors;

public class ErrorHandlerInterceptor : Interceptor
{
    private readonly ILogger<ErrorHandlerInterceptor> m_logger;
    
    public ErrorHandlerInterceptor(ILogger<ErrorHandlerInterceptor> p_logger)
    {
        m_logger = p_logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest                                        p_request,
        ClientInterceptorContext<TRequest, TResponse>   p_context,
        AsyncUnaryCallContinuation<TRequest, TResponse> p_continuation)
    {
        var call = p_continuation(p_request, p_context);

        return new AsyncUnaryCall<TResponse>(
            HandleResponse(call.ResponseAsync),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> p_inner)
    {
        try
        {
            return await p_inner;
        }
        catch (RpcException exception)
        {
            m_logger.LogError("Error in gRPC call: {ErrorMessage}", exception.Status.Detail);
            throw;
        }
    }
}