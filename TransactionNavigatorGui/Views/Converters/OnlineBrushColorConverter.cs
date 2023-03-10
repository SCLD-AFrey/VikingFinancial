using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace TransactionNavigatorGui.Views.Converters;

public class OnlineBrushColorConverter : IValueConverter
{
    public object Convert(object p_value, Type p_targetType, object p_parameter, CultureInfo p_culture)
    {
        if ((bool)p_value)
        {
            {
                return Brushes.Green;
            }
        }
        return Brushes.Red;
    }

    public object ConvertBack(object p_value, Type p_targetType, object p_parameter, CultureInfo p_culture)
    {
        throw new NotImplementedException();
    }
}