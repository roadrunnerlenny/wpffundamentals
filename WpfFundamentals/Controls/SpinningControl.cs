using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFundamentals.Controls
{
	public class SpinningControl : Control
	{
		static SpinningControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SpinningControl), new FrameworkPropertyMetadata(typeof(SpinningControl)));
		}

		public bool IsBusy
		{
			get { return (bool)GetValue(IsBusyProperty); }
			set { SetValue(IsBusyProperty, value); }
		}

		public static readonly DependencyProperty IsBusyProperty =
			DependencyProperty.Register("IsBusy", typeof(bool), typeof(SpinningControl), new UIPropertyMetadata(false));

	}
}
