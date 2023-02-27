using Serilog.Events;

namespace VikingFinancial.Gui.Models.DataStructures.Logging;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}