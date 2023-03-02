using System.IO;
using Avalonia.Collections;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace TransactionNavigator.Models.DataStructures;

public class CollectionSink : ILogEventSink
{
    private readonly ITextFormatter _textFormatter =
        new MessageTemplateTextFormatter("{Timestamp:HH:mm:ss} - {Message}{Exception}");

    public static AvaloniaList<ConsoleLogMessage> Events { get; set; } = new();

    public void Emit(LogEvent p_logEvent)
    {
        var renderer = new StringWriter();
        _textFormatter.Format(p_logEvent, renderer);

        var message = new ConsoleLogMessage
        {
            Text = renderer.ToString(),
            LogLevel = p_logEvent.Level
        };

        Events.Insert(0, message);
    }

    public static void SetCollection(AvaloniaList<ConsoleLogMessage> p_sink)
    {
        Events = p_sink;
    }
}
