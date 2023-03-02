using Serilog.Events;

namespace TransactionNavigator.Models.DataStructures;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}