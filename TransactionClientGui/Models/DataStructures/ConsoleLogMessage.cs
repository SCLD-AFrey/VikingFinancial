using Serilog.Events;

namespace TransactionClientGui.Models.DataStructures;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}