using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TransactionNavigator.Views.Converters;

public class ButtonHeightConverter : IValueConverter
{
    public object Convert(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        if ( p_value is not double doubleValue ) return 0.0d;

        if (p_parameter != null && p_parameter.Equals("half"))
        {
            return (doubleValue * .65d) / 2;
        }
        else
        {
            return doubleValue * .65d;
        }
    }

    public object ConvertBack(object? p_value, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        //throw new NotImplementedException();
        return null;
    }
}