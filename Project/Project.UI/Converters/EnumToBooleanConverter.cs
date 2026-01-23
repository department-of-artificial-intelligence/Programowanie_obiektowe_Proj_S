using System.Globalization;
using System.Windows.Data;

namespace Project.UI.Converters;

public class EnumToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not string parameterString)
            return false;

        if (value == null)
            return false;
        
        var enumType = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();
        
        if (!enumType.IsEnum)
            return false;

        if (!Enum.IsDefined(enumType, value))
            return false;

        var parameterValue = Enum.Parse(enumType, parameterString);
        return parameterValue.Equals(value);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not string parameterString)
            return Binding.DoNothing;

        if (value is not bool boolValue || !boolValue)
            return Binding.DoNothing;
        
        var enumType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        
        if (!enumType.IsEnum)
            return Binding.DoNothing;

        return Enum.Parse(enumType, parameterString);
    }
}
