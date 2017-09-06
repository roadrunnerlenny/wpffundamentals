using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;


namespace WpfFundamentals.Helper
{
	/// <summary>
	/// Verzögert die Ausführung der übergebenen Action um die angegebene Zeitspanne. Wenn innerhalb der angegebene Zeitspanne
	/// wieder Defer(..) ausgeführt wird, wird die übergebene Action erst nach Ablauf der neu übergebenenen Zeitspanne wieder aufgerufen).
	/// (D.h., die Ausführung verzögert sich wieder.)
	/// </summary>
	public sealed class DeferredAction : IDisposable
	{
		Timer timer;

		public DeferredAction(Action action, Dispatcher dispatcher)
		{
			this.timer = new Timer(new TimerCallback(o => dispatcher.Invoke(action)));
		}

		public void Defer(TimeSpan delay)
		{
			// Fire action when time elapses (with no subsequent calls).
			this.timer.Change(delay, TimeSpan.FromMilliseconds(-1));
		}

		#region IDisposable Member

		public void Dispose()
		{
			if (this.timer != null)
				this.timer.Dispose();
			this.timer = null;
		}

		#endregion
	}
}
