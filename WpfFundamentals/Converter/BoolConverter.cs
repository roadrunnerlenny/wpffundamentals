using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfFundamentals.Converter
{
	/// <summary>
	/// Konvertiert boolsche Werte (True oder False) in beliebig andere Objekte. 
	/// </summary>
	[ValueConversion(typeof(object), typeof(object))]
	public class BoolConverter : IValueConverter
	{
		public object TrueValue { get; set; }
		public object FalseValue { get; set; }

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool typedValue = (bool)value;
			return typedValue ? this.TrueValue : this.FalseValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return Binding.DoNothing;
		}

		#endregion
	}
}
