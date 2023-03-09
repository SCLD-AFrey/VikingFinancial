using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using Serilog;
using TextCopy;
using TransactionClient.Models.DataStructures;
using TransactionClient.Models.Services;
using ValkyrieFinancial.Protos;

namespace TransactionClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> m_logger;
    private readonly ClientProvisioner         m_clientProvisionerService;
    
    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger, ClientProvisioner p_clientProvisionerService)
    {
        m_logger = p_logger;
        m_clientProvisionerService = p_clientProvisionerService;
        m_logger.LogDebug("Instantiating MainWindowViewModel");
        WindowTitle = $"Transaction Navigator";
        CollectionSink.SetCollection(Messages);
        //CheckConnection();
    }
    [Reactive] public int SelectedIndex { get; set; } = 0;
    [Reactive] public string WindowTitle { get; set; }
    [Reactive] public bool IsOnline { get; set; } = false;
    [Reactive] public string OnlineMessage { get; set; } = "";
    [Reactive] public AvaloniaList<ConsoleLogMessage> Messages         { get; set; } = new ();
    [Reactive] public AvaloniaList<ConsoleLogMessage> SelectedMessages { get; set; } = new ();
    public void CopyMessages()
    {
        var selectedText = new StringBuilder();
        foreach ( var message in SelectedMessages )
        {
            selectedText.AppendLine(message.Text);
        }
        ClipboardService.SetTextAsync(selectedText.ToString());
    }

    public async Task CheckConnection()
    {
        m_logger.LogDebug("Checking connection");
        try
        {
            var client = m_clientProvisionerService.ProvisionConnectivityClient();
            var response = await client.CheckServerConnectionAsync(new Empty());

            switch (response.ResponseCase)
            {
                case G_ConnectCheckResponse.ResponseOneofCase.Success:
                    IsOnline = true;
                    m_logger.LogDebug("Connection Successful @ {S}", response.Success.Timestamp.ToDateTime());
                    break;
                case G_ConnectCheckResponse.ResponseOneofCase.Failure:
                    IsOnline = false;
                    m_logger.LogDebug("Connection Failed @ {S} - {M}", response.Failure.Timestamp.ToDateTime(),response.Failure.Message);
                    break;
            }
        }
        catch (Exception e)
        {
            IsOnline = false;
            m_logger.LogError(e, "Error checking connection");
        }
        OnlineMessage = IsOnline ? "Online" : "Offline";
        m_logger.LogDebug("Connection {S}", IsOnline.ToString());
    }
}