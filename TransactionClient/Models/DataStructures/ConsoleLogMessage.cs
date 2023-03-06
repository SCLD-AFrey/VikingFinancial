using Serilog.Events;

namespace TransactionClient.Models.DataStructures;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}