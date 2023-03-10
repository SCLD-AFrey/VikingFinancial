using Serilog.Events;

namespace TransactionNavigatorGui.Models.Logging;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}