using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfFundamentals.Helper
{
	public class ExceptionHelper
	{
		public static void InvokeAndIgnoreExceptions(Action a)
		{
			try
			{
				a.Invoke();
			}
			catch { ; }
		}
	}
}
