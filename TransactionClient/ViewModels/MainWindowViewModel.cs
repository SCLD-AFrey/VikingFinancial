using System.Text;
using Avalonia.Collections;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TextCopy;
using TransactionClient.Models.DataStructures;

namespace TransactionClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> m_logger;
    
    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating MainWindowViewModel");
        WindowTitle = $"Transaction Navigator";
        CollectionSink.SetCollection(Messages);
    }
    [Reactive] public string WindowTitle { get; set; }
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