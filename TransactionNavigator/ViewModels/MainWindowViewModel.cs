using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TransactionNavigator.Models;
using TransactionNavigator.Models.DataStructures;
using TransactionNavigator.Views;
using TextCopy;
using TransactionNavigator.Models.ServerInteraction;
using System.Net;
using Avalonia.Media;
using TransactionNavigator.Models.Services;
using VikingFinancial.Common;

namespace TransactionNavigator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> m_logger;
    private readonly Connectivity m_connectionService;
    private readonly ServerInfo m_serverInfo;
    private readonly  IServiceProvider            m_serviceProvider;
    private readonly ClientProvisioner m_clientProvisionerService;

    private MainWindowModel Model { get; }
    [Reactive] public string WindowTitle { get; set; }


    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger,
        MainWindowModel p_model, Connectivity p_connectionService, ServerInfo p_serverInfo,
        IServiceProvider p_serviceProvider, ClientProvisioner p_clientProvisionerService)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWindowViewModel");

        Model = p_model;
        m_connectionService = p_connectionService;
        m_serverInfo = p_serverInfo;
        m_serviceProvider = p_serviceProvider;
        m_clientProvisionerService = p_clientProvisionerService;

        WindowTitle = $"Transaction Navigator";

        OnlineColor = new SolidColorBrush(Colors.Red);
        IsOnline = CheckServer().Result;
        
        CollectionSink.SetCollection(Messages);
    }

    [Reactive] public bool IsOnline { get; set; } = false;
    [Reactive] public SolidColorBrush OnlineColor { get; set; }

    [Reactive] public int                             SelectedPageIndex     { get; set; }
    [Reactive] public AvaloniaList<ConsoleLogMessage> Messages         { get; set; } = new ();
    [Reactive] public AvaloniaList<ConsoleLogMessage> SelectedMessages { get; set; } = new ();

    public void CopyMessages()
    {
        var selectedText = new StringBuilder();

        foreach ( var message in SelectedMessages )
        {
            selectedText.AppendLine(message.Text);
        }
            
        ClipboardService.SetText(selectedText.ToString());
    }

    private async Task<bool> CheckServer()
    {
        m_logger.LogDebug("Connect To Server()");
        try
        {
            m_serverInfo.SetUnauthenticatedChannel(App.ServerCredentials!.ServerAddress, App.ServerCredentials!.ServerPort);
            await m_connectionService.TestServerConnectivityAsync();
            OnlineColor = new SolidColorBrush(Colors.Green);
            m_logger.LogDebug("Connection Passed");
            return true;
        } catch (Exception e)
        {
            OnlineColor = new SolidColorBrush(Colors.Red);
            m_logger.LogError(e, "Connection Failed");
            return false;
        }
    }
}