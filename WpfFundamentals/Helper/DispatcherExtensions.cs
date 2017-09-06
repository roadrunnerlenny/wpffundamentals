using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace WpfFundamentals.Helper
{
	/// <summary>
	/// Vereinfacht die Benutzung von Dispatcher.BeginInvoke per lambda-Ausdruck.
	/// </summary>
	public static class DispatcherExtensions
	{
		public static void BeginInvoke(this Dispatcher dispatcher, Action action, DispatcherPriority dispatcherPriority, params object[] args)
		{
			dispatcher.BeginInvoke(action, dispatcherPriority, args);
		}

		public static void BeginInvoke(this Dispatcher dispatcher, Action action, params object[] args)
		{
			dispatcher.BeginInvoke(action, args);
		}
	}
}
