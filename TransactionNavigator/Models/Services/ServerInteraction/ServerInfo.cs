using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using TransactionNavigator.Models.Interceptors;

namespace TransactionNavigator.Models.Services.ServerInteraction;

public class ServerInfo
{
    private readonly ErrorHandlerInterceptor m_interceptor;
    public ServerInfo(ErrorHandlerInterceptor p_interceptor)
    {
        m_interceptor = p_interceptor;
    }
    
    private           GrpcChannel?            Channel { get; set; }
    public            CallInvoker?            Invoker { get; private set; }

    public void SetUnauthenticatedChannelWithCredentials(string p_userName, string p_sessionId)
    {
        // ReSharper disable once InconsistentNaming 
        var credentials = CallCredentials.FromInterceptor((_, p_metadata) =>
                                                          {
                                                              p_metadata.Add("UserName", p_userName);
                                                              p_metadata.Add("SessionId", p_sessionId);
  
                                                              return Task.CompletedTask;
                                                          });

        var handler = new SocketsHttpHandler
                      {
                          PooledConnectionIdleTimeout    = Timeout.InfiniteTimeSpan,
                          KeepAlivePingDelay             = TimeSpan.FromSeconds(60),
                          KeepAlivePingTimeout           = TimeSpan.FromSeconds(30),
                          EnableMultipleHttp2Connections = true
                      };

        var methodConfig = new MethodConfig
                           {
                               Names = { MethodName.Default },
                               RetryPolicy = new RetryPolicy
                                             {
                                                 MaxAttempts          = 3,
                                                 InitialBackoff       = TimeSpan.FromSeconds(1),
                                                 MaxBackoff           = TimeSpan.FromSeconds(2.5),
                                                 BackoffMultiplier    = 1.5,
                                                 RetryableStatusCodes = { StatusCode.Unavailable }
                                             }
                           };

        Channel = GrpcChannel.ForAddress($"https://{Channel!.Target}", new GrpcChannelOptions
                                                                        {
                                                                            HttpHandler = handler,
                                                                            Credentials =
                                                                                ChannelCredentials.Create(new SslCredentials(),
                                                                                    credentials),
                                                                            ServiceConfig = new ServiceConfig
                                                                                            {
                                                                                                MethodConfigs = { methodConfig }
                                                                                            }
                                                                        });
        
        Invoker = Channel.Intercept(m_interceptor);
    }
    
    public void SetUnauthenticatedChannel(string p_target, ushort p_port, string p_userName = "", string p_sessionId = "")
    {
        var hostAddresses = Dns.GetHostEntry(p_target, AddressFamily.InterNetwork);
        
        var credentials = CallCredentials.FromInterceptor((_, p_metadata) =>
                                                          {
                                                              if ( !string.IsNullOrEmpty(p_userName) && !string.IsNullOrEmpty(p_sessionId) )
                                                              {
                                                                  p_metadata.Add("UserName", p_userName);
                                                                  p_metadata.Add("SessionId", p_sessionId);
                                                              }

                                                              return Task.CompletedTask;
                                                          });

        var handler = new SocketsHttpHandler
                      {
                          PooledConnectionIdleTimeout    = Timeout.InfiniteTimeSpan,
                          KeepAlivePingDelay             = TimeSpan.FromSeconds(60),
                          KeepAlivePingTimeout           = TimeSpan.FromSeconds(30),
                          EnableMultipleHttp2Connections = true
                      };

        var methodConfig = new MethodConfig
                           {
                               Names = { MethodName.Default },
                               RetryPolicy = new RetryPolicy
                                             {
                                                 MaxAttempts          = 3,
                                                 InitialBackoff       = TimeSpan.FromSeconds(1),
                                                 MaxBackoff           = TimeSpan.FromSeconds(2.5),
                                                 BackoffMultiplier    = 1.5,
                                                 RetryableStatusCodes = { StatusCode.Unavailable }
                                             }
                           };

        Channel = GrpcChannel.ForAddress($"dns://{hostAddresses.HostName}:{p_port}", new GrpcChannelOptions
                                                                                     {
                                                                                         HttpHandler = handler,
                                                                                         Credentials =
                                                                                             ChannelCredentials.Create(new SslCredentials(),
                                                                                                 credentials),
                                                                                         ServiceConfig = new ServiceConfig
                                                                                             {
                                                                                                 MethodConfigs = { methodConfig }
                                                                                             }
                                                                                     });

        Invoker = Channel.Intercept(m_interceptor);
    }

     
    public void SetAuthenticatedChannel(string p_token, string p_userName, string p_sessionId)
    {
        // ReSharper disable once InconsistentNaming 
        var credentials = CallCredentials.FromInterceptor((_, p_metadata) =>
                                                          {
                                                              if ( !string.IsNullOrEmpty(p_token) )
                                                              {
                                                                  p_metadata.Add("Authorization", $"Bearer {p_token}");
                                                                  p_metadata.Add("UserName", p_userName);
                                                                  p_metadata.Add("SessionId", p_sessionId);
                                                              }

                                                              return Task.CompletedTask;
                                                          });

        var handler = new SocketsHttpHandler
                      {
                          PooledConnectionIdleTimeout    = Timeout.InfiniteTimeSpan,
                          KeepAlivePingDelay             = TimeSpan.FromSeconds(60),
                          KeepAlivePingTimeout           = TimeSpan.FromSeconds(30),
                          EnableMultipleHttp2Connections = true
                      };

        var methodConfig = new MethodConfig
                           {
                               Names = { MethodName.Default },
                               RetryPolicy = new RetryPolicy
                                             {
                                                 MaxAttempts          = 3,
                                                 InitialBackoff       = TimeSpan.FromSeconds(1),
                                                 MaxBackoff           = TimeSpan.FromSeconds(2.5),
                                                 BackoffMultiplier    = 1.5,
                                                 RetryableStatusCodes = { StatusCode.Unavailable }
                                             }
                           };

        Channel = GrpcChannel.ForAddress($"https://{Channel!.Target}", new GrpcChannelOptions
                                                                        {
                                                                            HttpHandler = handler,
                                                                            Credentials =
                                                                                ChannelCredentials.Create(new SslCredentials(),
                                                                                    credentials),
                                                                            ServiceConfig = new ServiceConfig
                                                                                            {
                                                                                                MethodConfigs = { methodConfig }
                                                                                            }
                                                                        });
        
        Invoker = Channel.Intercept(m_interceptor);
    }
}