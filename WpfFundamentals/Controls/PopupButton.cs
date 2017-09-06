using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace WpfFundamentals.Controls
{
	public class PopupButton : ContentControl
	{
		ToggleButton ToggleButton { get; set; }

		static PopupButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupButton), new FrameworkPropertyMetadata(typeof(PopupButton)));
		}

		public string PopupHeader
		{
			get { return (string)GetValue(PopupHeaderProperty); }
			set
			{
				SetValue(PopupHeaderProperty, value);
			}
		}

		public static readonly DependencyProperty PopupHeaderProperty =
			DependencyProperty.Register("PopupHeader", typeof(string), typeof(PopupButton), new UIPropertyMetadata(OnPopupHeaderChanged));

		static void OnPopupHeaderChanged(DependencyObject dep, DependencyPropertyChangedEventArgs args)
		{
			PopupButton myself = (PopupButton)dep;
			myself.HasPopupHeader = !String.IsNullOrWhiteSpace((string)args.NewValue);
		}

		public bool HasPopupHeader
		{
			get { return (bool)GetValue(HasPopupHeaderProperty); }
			private set { SetValue(HasPopupHeaderPropertyKey, value); }
		}

		static readonly DependencyPropertyKey HasPopupHeaderPropertyKey =
			DependencyProperty.RegisterReadOnly("HasPopupHeader", typeof(bool), typeof(PopupButton), new UIPropertyMetadata(false));

		public static readonly DependencyProperty HasPopupHeaderProperty = HasPopupHeaderPropertyKey.DependencyProperty;

		public object PopupContent
		{
			get { return (object)GetValue(PopupContentProperty); }
			set { SetValue(PopupContentProperty, value); }
		}

		public static readonly DependencyProperty PopupContentProperty =
			DependencyProperty.Register("PopupContent", typeof(object), typeof(PopupButton), new UIPropertyMetadata(null));

		public double PopupWidth
		{
			get { return (double)GetValue(PopupWidthProperty); }
			set { SetValue(PopupWidthProperty, value); }
		}

		public static readonly DependencyProperty PopupWidthProperty =
			DependencyProperty.Register("PopupWidth", typeof(double), typeof(PopupButton), new UIPropertyMetadata(0.0));

		public double PopupHeight
		{
			get { return (double)GetValue(PopupHeightProperty); }
			set { SetValue(PopupHeightProperty, value); }
		}

		public static readonly DependencyProperty PopupHeightProperty =
			DependencyProperty.Register("PopupHeight", typeof(double), typeof(PopupButton), new UIPropertyMetadata(0.0));

		public PlacementMode PopupPlacement
		{
			get { return (PlacementMode)GetValue(PopupPlacementProperty); }
			set { SetValue(PopupPlacementProperty, value); }
		}

		public static readonly DependencyProperty PopupPlacementProperty =
			DependencyProperty.Register("PopupPlacement", typeof(PlacementMode), typeof(PopupButton), new UIPropertyMetadata(PlacementMode.Bottom));


		public double HorizontalOffset
		{
			get { return (double)GetValue(HorizontalOffsetProperty); }
			set { SetValue(HorizontalOffsetProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(PopupButton), new UIPropertyMetadata(0.0));

		public double VerticalOffset
		{
			get { return (double)GetValue(VerticalOffsetProperty); }
			set { SetValue(VerticalOffsetProperty, value); }
		}

		// Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty VerticalOffsetProperty =
			DependencyProperty.Register("VerticalOffset", typeof(double), typeof(PopupButton), new UIPropertyMetadata(0.0));



		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(PopupButton), new UIPropertyMetadata(null));

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object), typeof(PopupButton), new UIPropertyMetadata(null));



		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			ApplyTemplateForCloseButton();
			ApplyTemplateForToggleButton();

		}

		public void ClosePopup()
		{
			if (this.ToggleButton != null)
				this.ToggleButton.IsChecked = false;
		}

		public void OpenPopup()
		{
			if (this.ToggleButton != null)
				this.ToggleButton.IsChecked = true;
		}

		private void ApplyTemplateForCloseButton()
		{
			Button closeButton = GetTemplateChild("PART_CloseButton") as Button;
			if (closeButton != null)
			{
				AttachCloseButtonClickEventToToggleButton(closeButton);
			}
		}

		private void AttachCloseButtonClickEventToToggleButton(Button closeButton)
		{
			this.ToggleButton = GetTemplateChild("PART_Toggle") as ToggleButton;
			closeButton.Click += (s, e) =>
			{
				if (this.ToggleButton != null)
					this.ToggleButton.IsChecked = false;
			};
		}

		private void ApplyTemplateForToggleButton()
		{
			ToggleButton toggleButton = GetTemplateChild("PART_Toggle") as ToggleButton;
			if (toggleButton != null)
			{
				toggleButton.Click += (s, e) =>
				{
					RaiseEvent(new RoutedEventArgs(PopupButton.OpenPopupClickEvent));
				};
			}

		}

		public static readonly RoutedEvent OpenPopupClickEvent =
			EventManager.RegisterRoutedEvent("OpenPopupClick", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(PopupButton));

		public event RoutedEventHandler OpenPopupClick
		{
			add { AddHandler(OpenPopupClickEvent, value); }
			remove { RemoveHandler(OpenPopupClickEvent, value); }
		}



	}
}
