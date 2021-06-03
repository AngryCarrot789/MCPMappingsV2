using System;
using System.Globalization;
using System.Windows.Data;

namespace MCPMappingsV2.Converters {
    public class EnumBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is string parameterString) {
                if (Enum.IsDefined(value.GetType(), value) == false) {
                    return "";
                }

                return Enum.Parse(value.GetType(), parameterString).Equals(value);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is string parameterString) {
                return Enum.Parse(targetType, parameterString);
            }

            return "";
        }
    }
}
