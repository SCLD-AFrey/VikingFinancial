using Serilog.Events;

namespace VikingFinancial.App.Gui.Models.Logging;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}