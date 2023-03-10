using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Serilog.Events;

namespace TransactionNavigatorGui.Views.Converters;

public class LogEventLevelToColorConverter : IValueConverter
{
    public object? Convert(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        if ( p_value is not LogEventLevel logLevel ) return null;

        return logLevel switch
        {
            LogEventLevel.Debug       => Brushes.DarkCyan,
            LogEventLevel.Information => Brushes.DodgerBlue,
            LogEventLevel.Warning     => Brushes.DarkGoldenrod,
            LogEventLevel.Error       => Brushes.IndianRed,
            LogEventLevel.Fatal       => Brushes.DarkRed,
            _                         => Brushes.Black
        };
    }

    object? IValueConverter.ConvertBack(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        //throw new NotImplementedException();
        return null;
    }
}