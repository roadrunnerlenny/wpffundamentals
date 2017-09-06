using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfFundamentals.Converter.NullableConverter
{
	[ValueConversion(typeof(int?), typeof(string))]
	public class NullableIntConverter : NullableConverter<int>
	{
		protected override bool TryParse(string value, System.Globalization.CultureInfo culture, out int result)
		{
			return int.TryParse(value, out result);
		}
	}
}
