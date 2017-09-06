

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfFundamentals.Converter
{	
	public class DebugConverter: IValueConverter, IMultiValueConverter
	{    
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
#if DEBUG
			System.Diagnostics.Debugger.Break();
#endif
			return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
#if DEBUG
			System.Diagnostics.Debugger.Break();
#endif
			return value; 
        }

		#region IMultiValueConverter Member

		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
#if DEBUG
			System.Diagnostics.Debugger.Break();
#endif
			return values;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
#if DEBUG
			System.Diagnostics.Debugger.Break();
#endif
			return new object [] { Binding.DoNothing };
		}

		#endregion
	
	}
}

