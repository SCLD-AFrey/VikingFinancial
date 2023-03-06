using System;
using System.Text;
using Avalonia.Collections;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TextCopy;
using TransactionClient.Models;
using TransactionClient.Models.DataStructures;
using TransactionClient.Models.ServerInteraction;
using TransactionClient.Models.Services;

namespace TransactionClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> m_logger;
    private readonly Connectivity m_connectionService;
    private readonly ServerInfo m_serverInfo;
    private readonly IServiceProvider m_serviceProvider;
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
       
        CollectionSink.SetCollection(Messages);
    }

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
        ClipboardService.SetTextAsync(selectedText.ToString());
    }
}