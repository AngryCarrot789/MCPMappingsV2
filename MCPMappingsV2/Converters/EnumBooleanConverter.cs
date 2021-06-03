using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MCPMappingsV2.Converters {
    public class EnumBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is string enumType) {
                if (!Enum.IsDefined(value.GetType(), value)) {
                    return DependencyProperty.UnsetValue;
                }

                return Enum.Parse(value.GetType(), enumType).Equals(value);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is string parameterString) {
                return Enum.Parse(targetType, parameterString);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
