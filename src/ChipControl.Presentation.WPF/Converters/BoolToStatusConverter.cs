using System;
using System.Globalization;
using System.Windows.Data;

namespace ChipControl.Presentation.WPF.Converters;

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "Ativo" : "Inativo";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value == "Ativo";
    }
}
