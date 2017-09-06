using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections.ObjectModel;
using WpfFundamentals.Helper;

namespace WpfFundamentals.Converter
{
	/// <summary>
	/// Konvertiert eine Liste in einen einzigen String, wobei für jedes Element der List die ToString() Methode zur Stringerzeugung genutzt wird.
	/// Seperator gibt das Trennzeichen an. 
	/// DefaultReturnValue definiert den Standardwert für leere oder nicht initialisierte Listen. 
	/// Sorted (Ja/Nein) gibt an, ob die Ausgabe sortiert werden soll. 
	/// </summary>
	public class ListToStringConverter : IValueConverter
	{
		public string DefaultReturnValue { get; set; }

		public char Seperator { get; set; }

		IEnumerable<object> InputList { get; set; }

		bool HasInputList
		{
			get { return InputList != null && InputList.Count() > 0;  }
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			InputList = value as IEnumerable<object>;
			SetDefaultSeperatorIfNecessary();

			if (HasInputList)				
				return String.Join(Seperator.ToString() + " ", InputList.Select(o => o.ToString()).ToArray());
			else
				return DefaultReturnValue;
		}

		void SetDefaultSeperatorIfNecessary()
		{
			if (Seperator == 0) Seperator = ',';
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return Binding.DoNothing;
		}

	}
}
