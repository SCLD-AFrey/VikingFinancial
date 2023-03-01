using System;
using System.Linq;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.Threading;
using Grpc.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using TransactionClient.Models;
using TransactionClient.Models.DataStructures;
using TransactionClient.Models.Services;
using TransactionClient.Views;
using VersionInfo = TransactionClient.Models.Services.VersionInfo;

namespace TransactionClient.ViewModels;

public class UserLoginViewModel : ViewModelBase
{
    private readonly ILogger<UserLoginViewModel> m_logger;
    private readonly UserLoginModel m_model;
    private readonly  VersionInfo                 m_versionInfoService;

    private readonly IServiceProvider m_serviceProvider;

    //private readonly  VersionInfo                 m_versionInfoService;
    [Reactive] public string? ValidationError { get; set; }
    [Reactive] public string? ServerAddress { get; set; }
    [Reactive] public string? Port { get; set; }
    [Reactive] public string? Username { get; set; }

    [Reactive] public string? UserPassword { get; set; }

    //public            string                      VersionNumber             => m_versionInfoService.VersionNumber;
    [Reactive] public bool IsBusy { get; set; }

    public int AttemptedLoginCount { get; set; }

    //public            ChangePasswordViewModel     ChangePasswordViewModel { get; set; }
    public UserLoginViewModel(ILogger<UserLoginViewModel> p_logger, UserLoginModel p_model, IServiceProvider p_serviceProvider, VersionInfo p_versionInfoService)
    {
        m_logger = p_logger;
        m_model = p_model;
        m_serviceProvider = p_serviceProvider;
        m_versionInfoService = p_versionInfoService;
        m_logger.LogDebug("Instantiating UserLoginViewModel");
        
        var clientConfig = m_model.GetClientConfig();
        ServerAddress   = clientConfig?.ServerAddress ?? string.Empty;
        Port            = clientConfig?.Port          ?? string.Empty;
    }
    
    public UserLoginViewModel(ILogger<UserLoginViewModel>             p_logger,              
        VersionInfo                             p_versionInfoService, 
        IServiceProvider                        p_serviceProvider, 
        UserLoginModel                          p_model)
    {
        m_logger                  = p_logger;
        m_versionInfoService      = p_versionInfoService;
        m_serviceProvider         = p_serviceProvider;
        m_model                   = p_model;
        
        m_logger.LogDebug("Instantiating UserLoginViewModel");
        
        var clientConfig = m_model.GetClientConfig();
        ServerAddress   = clientConfig?.ServerAddress ?? string.Empty;
        Port            = clientConfig?.Port          ?? string.Empty;
    }
    
        
    [DependsOn(nameof(IsBusy))]
    public bool CanClickLoadPairingKey(object p_parameters)
    {
        return !IsBusy;
    }
    
    [DependsOn(nameof(IsBusy))]
    public bool CanClickLogin(object p_parameters)
    {
        return !IsBusy;
    }
    
    public async Task ClickLogin(Window p_loginWindow)
    {
        try
        {
            ValidationError = string.Empty;
                        
            IsBusy             = true;
            
            await Task.Delay(m_model.GetLoginDelayTime(AttemptedLoginCount));

            var serverLoginObject = new ServerLoginCredentials(ServerAddress!, ushort.Parse(Port!), Username!.Trim(), UserPassword!);

            try
            {

                await m_model.AttemptLogin(serverLoginObject);

                m_logger.LogDebug("Login was successful for {Username}", Username);
                await m_model.SaveClientConfig(ServerAddress!, Port!);

                await m_model.GetPostLoginData();

                IsBusy             = true;

                await Dispatcher.UIThread.InvokeAsync(() =>
                                                      {
                                                          var mainWindow = m_serviceProvider.GetRequiredService<MainWindowView>();
                                                          mainWindow.Show();
                                                          p_loginWindow.Close();
                                                      });
            }
            catch ( RpcException exception )
            {
                AttemptedLoginCount++;
                IsBusy          = false;
                ValidationError = $"Server could not be reached - Status: {exception.Status.StatusCode}";
            }
            catch ( SecurityException exception )
            {
                AttemptedLoginCount++;

                // shift window up a little to make room for the 1280x768 supported min display, showing the Login buttons - Comment by jmccoard on 02/16/2022 @ 16:51:58
                p_loginWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                p_loginWindow.Position              = new PixelPoint(p_loginWindow.Position.X, p_loginWindow.Position.Y - 120);

                IsBusy             = false;
                ValidationError    = exception.Message;
            }
            catch ( CryptographicException exception )
            {
                AttemptedLoginCount++;
                IsBusy = false;
                ValidationError =
                    "The ConfigOS Server validation certificate was manually removed. In order to reinstall, please run the ConfigOS Client as an administrator.";
                m_logger.LogInformation("{ValidationError} Error: {ExceptionMessage}", ValidationError, exception.Message);
            }
            catch ( Exception exception ) when ( exception is InvalidCredentialException or AuthenticationException )
            {
                AttemptedLoginCount++;
                IsBusy          = false;
                ValidationError = exception.Message;
            }
        }
        catch ( Exception e )
        {
            AttemptedLoginCount++;
            IsBusy          = false;
            ValidationError = $"Error attempting to login: {e.Message}";
        }
    }
    
    [DependsOn(nameof(IsBusy))]
    public bool CanClickChangePassword(object p_parameters)
    {
        return !IsBusy;
    }
    
    public async Task ClickChangePassword(Window p_loginWindow)
    {
        try
        {
            ValidationError = string.Empty;
                        
            IsBusy             = true;
            await Task.Delay(m_model.GetLoginDelayTime(AttemptedLoginCount));

            try
            {
                await m_model.SaveClientConfig(ServerAddress!, Port!);
                await m_model.GetPostLoginData();


                IsBusy             = true;

                await Dispatcher.UIThread.InvokeAsync(() =>
                                                      {
                                                          var mainWindow = m_serviceProvider.GetRequiredService<MainWindowView>();
                                                          mainWindow.Show();
                                                          p_loginWindow.Close();
                                                      });
            }
            catch ( RpcException exception )
            {
                AttemptedLoginCount++;
                IsBusy          = false;
                ValidationError = $"Server could not be reached - Status: {exception.Status.StatusCode}";
            }
            catch ( SecurityException exception )
            {
                AttemptedLoginCount++;
                // shift window up a little to make room for the 1280x768 supported min display, showing the Login buttons - Comment by jmccoard on 02/16/2022 @ 16:51:58
                p_loginWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                p_loginWindow.Position              = new PixelPoint(p_loginWindow.Position.X, p_loginWindow.Position.Y - 120);
            
                IsBusy             = false;
                ValidationError    = exception.Message;
            }
            catch ( CryptographicException exception )
            {
                AttemptedLoginCount++;
                IsBusy = false;
                ValidationError = $"The ConfigOS Server validation certificate was manually removed. In order to reinstall, please run the ConfigOS Client as an administrator.";
                m_logger.LogInformation("{ValidationError} Error: {ExceptionMessage}", ValidationError, exception.Message);
            }
            catch ( Exception exception ) when ( exception is InvalidCredentialException or AuthenticationException )
            {
                AttemptedLoginCount++;
                IsBusy          = false;
                ValidationError = exception.Message;
            }
        }
        catch ( Exception e )
        {
            AttemptedLoginCount++;
            IsBusy          = false;
            ValidationError = $"Error attempting to login: {e.Message}";
        }
    }

    private static bool PortIsInvalid(string? p_port)
    {
        return !string.IsNullOrEmpty(p_port) && p_port.Any(p_char => !char.IsDigit(p_char));
    }
    
    private static bool AddressIsInvalid(string? p_value)
    {
        if ( string.IsNullOrEmpty(p_value) ) return false;

        //List of disallowed characters in DNS host names, excluding : and . as those might be needed for IP address
        var invalidChars = new[] { ',', '~', '!', '@', '\\', ';', '#', '$', '%', '^', '&', '\'', '(', ')', '{', '}', ' ', '*' };
        return invalidChars.Any(p_value.Contains) || p_value.EndsWith('-') || p_value.EndsWith('.') ||
               !char.IsLetterOrDigit(p_value[0]);
    }

    public void ClickCloseCommandCenter()
    {
        m_model.CloseCommandCenter();
    }
}