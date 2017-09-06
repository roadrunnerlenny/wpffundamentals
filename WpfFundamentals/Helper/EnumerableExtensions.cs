using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WpfFundamentals.ViewModelBase;

namespace WpfFundamentals.Helper
{
	public static class EnumerableExtensions
	{

		/// <summary>
		/// Liefert eine Aufzählung (IEnumberable) der Models zurück.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Models<T>(this IEnumerable<ViewModel<T>> source)
		{
			var models = from vm in source
						 select vm.Model;
			return models;
		}

		/// <summary>
		/// Führt die angegebene Aktion für jedes Element der Liste aus. 
		/// </summary>		
		public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source != null)
			{
				foreach (var i in source)
					action(i);
			}
			return source;
		}

		/// <summary>
		/// Erzeugt eine ObservableCollection aus der übergebenen Aufzählung
		/// </summary>		
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			return new ObservableCollection<T>(source);
		}

		/// <summary>
		/// Erzeugt eine ReadOnlyCollection aus der übergebenen Aufzählung
		/// </summary>
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
		{
			return new ReadOnlyCollection<T>(source.ToArray());
		}

		/// <summary>
		/// Erzeugt eine ReadOnlyObservableCollection aus der übergebenen Aufzählung
		/// </summary>		
		public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> source)
		{
			return new ReadOnlyObservableCollection<T>(new ObservableCollection<T>(source));
		}

		/// <summary>
		/// Erzeugt eine ReadOnlyObservableCollection aus der übergebenen ObservableCollection
		/// </summary>
		public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this ObservableCollection<T> source)
		{
			return new ReadOnlyObservableCollection<T>(source);
		}

	}
}
