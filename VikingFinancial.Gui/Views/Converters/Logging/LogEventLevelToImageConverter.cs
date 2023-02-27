using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Material.Icons;
using Serilog.Events;

namespace VikingFinancial.Gui.Views.Converters.Logging;

public class LogEventLevelToImageConverter : IValueConverter
{
    public object? Convert(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        if ( p_value is not LogEventLevel logLevel ) return null;

        return logLevel switch
        {
            LogEventLevel.Debug       => MaterialIconKind.Bug,
            LogEventLevel.Information => MaterialIconKind.Information,
            LogEventLevel.Warning     => MaterialIconKind.Alert,
            LogEventLevel.Error       => MaterialIconKind.AlertCircle,
            LogEventLevel.Fatal       => MaterialIconKind.AlertOctagon,
            _                         => MaterialIconKind.Information
        };
    }

    public object? ConvertBack(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        throw new NotImplementedException();
    }
}