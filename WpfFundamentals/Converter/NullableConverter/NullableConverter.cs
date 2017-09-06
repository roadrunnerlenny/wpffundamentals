using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfFundamentals.Converter.NullableConverter
{
	/// <summary>
	/// Problem: Textboxen mit Bindungen an nullable Basistypen haben Probleme, wenn die Textbox geleert wird - dies funktioniert nicht richtig.
	/// Lösung: Der NullableConvert Konvertiert einen leeren string aus der Textbox in eine richtige null des entsprechenden BasisTypen, so dass der Fehler
	/// nicht mehr auftritt. 
	/// </summary>
	/// <typeparam name="T">Ein nullable Basistyp (int?, short?, ...)</typeparam>
	public abstract class NullableConverter<T> : IValueConverter where T : struct
	{
		#region IValueConverter Member

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value != null ? value.ToString() : null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string stringValue = value as string;
			if (string.IsNullOrEmpty(stringValue))
				return null;

			T parsedValue = default(T);
			if (TryParse(stringValue, culture, out parsedValue))
				return (T?)parsedValue;
			return value;
		}

		#endregion

		protected abstract bool TryParse(string value, System.Globalization.CultureInfo culture, out T result);
	}
}
