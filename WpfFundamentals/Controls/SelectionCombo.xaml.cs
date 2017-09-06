using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;

namespace WpfFundamentals.Controls
{
	/// <summary>
	/// Interaktionslogik für SelectionCombo.xaml
	/// </summary>
	public partial class SelectionCombo : UserControl
	{
		public IList ItemsSource
		{
			get
			{
				return (IList)GetValue(ItemsSourceProperty);
			}
			set
			{
				this.InternalComboBox.ItemsSource = (IList)value;
				SetValue(ItemsSourceProperty, value);
			}
		}

		public static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
			"ItemsSource", typeof(IList), typeof(SelectionCombo), new UIPropertyMetadata(null));

		public string DisplayMemberPath
		{
			get { return (string)GetValue(DisplayMemberPathProperty); }
			set { SetValue(DisplayMemberPathProperty, value); }
		}

		public static DependencyProperty DisplayMemberPathProperty =
			DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(null));

        //new 


        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SelectionCombo), new UIPropertyMetadata(null));
        
        //endnew

		public string IsSelectedMemberPath
		{
			get { return (string)GetValue(IsSelectedMemberPathProperty); }
			set { SetValue(IsSelectedMemberPathProperty, value); }
		}

		public static readonly DependencyProperty IsSelectedMemberPathProperty =
			DependencyProperty.Register("IsSelectedMemberPath", typeof(string), typeof(SelectionCombo),
			new UIPropertyMetadata(null));

		public string IsEnabledMemberPath
		{
			get { return (string)GetValue(IsEnabledMemberPathProperty); }
			set { SetValue(IsEnabledMemberPathProperty, value); }
		}

		public static readonly DependencyProperty IsEnabledMemberPathProperty =
			DependencyProperty.Register("IsEnabledMemberPath", typeof(string), typeof(SelectionCombo),
			new UIPropertyMetadata(null));

		public string IsCheckBoxVisibleMemberPath
		{
			get { return (string)GetValue(IsCheckBoxVisibleMemberPathProperty); }
			set { SetValue(IsCheckBoxVisibleMemberPathProperty, value); }
		}

		public static readonly DependencyProperty IsCheckBoxVisibleMemberPathProperty =
			DependencyProperty.Register("IsCheckBoxVisibleMemberPath", typeof(string), typeof(SelectionCombo),
			new UIPropertyMetadata(null));

		public string IsExpandedNodeMemberPath
		{
			get { return (string)GetValue(IsExpandedNodeMemberPathProperty); }
			set { SetValue(IsExpandedNodeMemberPathProperty, value); }
		}

		public static readonly DependencyProperty IsExpandedNodeMemberPathProperty =
			DependencyProperty.Register("IsExpandedNodeMemberPath", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(null));

		public string ChildrenNodeMemberPath
		{
			get { return (string)GetValue(ChildrenNodeMemberPathProperty); }
			set { SetValue(ChildrenNodeMemberPathProperty, value); }
		}

		public static readonly DependencyProperty ChildrenNodeMemberPathProperty =
			DependencyProperty.Register("ChildrenNodeMemberPath", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(null));

        ///// <summary>
        /////Gets or sets a property to bind the selected items of the SelectionContainer
        ///// </summary>
        //public object ValidationBinding
        //{
        //    get { return (object)GetValue(ValidationBindingProperty); }
        //    set { SetValue(ValidationBindingProperty, value); }
        //}

        //public static DependencyProperty ValidationBindingProperty =
        //    DependencyProperty.Register("ValidationBinding", typeof(object), typeof(SelectionCombo), new UIPropertyMetadata(null));

		/// <summary>
		/// Defines if the first entry in the list is a "Select all" entry, which when clicked selects all elements in the list
		/// </summary>
		public bool HasSelectAll
		{
			get { return (bool)GetValue(HasSelectAllProperty); }
			set { SetValue(HasSelectAllProperty, value); }
		}

		public static readonly DependencyProperty HasSelectAllProperty =
			DependencyProperty.Register("HasSelectAll", typeof(bool), typeof(SelectionCombo), new PropertyMetadata(false));

		/// <summary>
		/// Determines the state of the "SelectAll" Checkbox
		/// </summary>
		public bool SelectAllIsChecked
		{
			get { return (bool)GetValue(SelectAllIsCheckedProperty); }
			set { SetValue(SelectAllIsCheckedProperty, value); }
		}

		public static readonly DependencyProperty SelectAllIsCheckedProperty =
			DependencyProperty.Register("SelectAllIsChecked", typeof(bool), typeof(SelectionCombo), new PropertyMetadata(false));

		/// <summary>
		/// Gets or sets the text for the Select-All element (only necessary if HasSelectAll is set to true)
		/// </summary>	
		public string SelectAllText
		{
			get { return (string)GetValue(SelectAllTextProperty); }
			set { SetValue(SelectAllTextProperty, value); }
		}

		public static DependencyProperty SelectAllTextProperty =
			DependencyProperty.Register("SelectAllText", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(string.Empty));

		/// <summary>
		///Gets or sets the text displayed in the ComboBox
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(string.Empty, (d, v) => { }, coerceTextCallback));

		private static object coerceTextCallback(DependencyObject d, object value)
		{
			if (String.IsNullOrEmpty((string)value))
			{
				value = (string)d.GetValue(DefaultTextProperty);
			}
			return value;
		}

		/// <summary>
		///Gets or sets the text displayed in the ComboBox if there are no selected items
		/// </summary>
		public string DefaultText
		{
			get { return (string)GetValue(DefaultTextProperty); }
			set { SetValue(DefaultTextProperty, value); }
		}

		public static readonly DependencyProperty DefaultTextProperty =
			 DependencyProperty.Register("DefaultText", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(string.Empty));

		/// <summary>
		/// Defines if "ContentScroll" is possible - this improves the performances if the combobox contains many items,
		/// but may lead to unwanted scrolling behaviour
		/// </summary>
		public bool CanContentScroll
		{
			get { return (bool)GetValue(CanContentScrollProperty); }
			set { SetValue(CanContentScrollProperty, value); }
		}

		public static readonly DependencyProperty CanContentScrollProperty =
			DependencyProperty.Register("CanContentScroll", typeof(bool), typeof(SelectionCombo), new PropertyMetadata(false));

		/// <summary>
		/// Defines if the SelectionBox is Editable
		/// If yes, a textbox is displayed which can be edited
		/// The Text-Property in the Control should then have a two-way binding
		/// </summary>
		public bool IsEditable
		{
			get { return (bool)GetValue(IsEditableProperty); }
			set { SetValue(IsEditableProperty, value); }
		}

		public static readonly DependencyProperty IsEditableProperty =
			DependencyProperty.Register("IsEditable", typeof(bool), typeof(SelectionCombo), new PropertyMetadata(false));

		/// <summary>
		/// Defines a shortcut to SelectAll items
		/// </summary>
		public string SelectAllShortcut
		{
			get { return (string)GetValue(SelectAllShortcutProperty); }
			set { SetValue(SelectAllShortcutProperty, value); }
		}

		public static readonly DependencyProperty SelectAllShortcutProperty =
			 DependencyProperty.Register("SelectAllShortcut", typeof(string), typeof(SelectionCombo), new UIPropertyMetadata(string.Empty));

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
																		"CommandParameter", typeof(object), typeof(SelectionCombo),
																		new PropertyMetadata(new PropertyChangedCallback(OnCommandParameterChanged)));

		private static void OnCommandParameterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			SelectionCombo self = (SelectionCombo)sender;
			self.OnCommandParameterChanged(args.OldValue, args.NewValue);
		}

		private void OnCommandParameterChanged(object oldValue, object newValue)
		{
			UpdateCanExecute();
		}
		/// <summary>
		/// The command, triggered when the drop down is closed
		/// </summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
																		"Command", typeof(ICommand), typeof(SelectionCombo),
																		new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));

		private static void OnCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			SelectionCombo self = (SelectionCombo)sender;
			self.OnCommandChanged(args.OldValue as ICommand, args.NewValue as ICommand);
		}

		private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
		{
			if (oldCommand != null)
				oldCommand.CanExecuteChanged -= NewCommand_CanExecuteChanged;

			if (newCommand != null)
				newCommand.CanExecuteChanged += NewCommand_CanExecuteChanged;

			UpdateCanExecute();
		}

		void NewCommand_CanExecuteChanged(object sender, EventArgs e)
		{
			UpdateCanExecute();
		}

		void UpdateCanExecute()
		{
			SetValue(CanExecutePropertyKey, this.Command != null && this.Command.CanExecute(this.CommandParameter));
		}

		public bool CanExecute
		{
			get { return (bool)GetValue(CanExecuteProperty); }
		}

		static readonly DependencyPropertyKey CanExecutePropertyKey = DependencyProperty.RegisterReadOnly("CanExecute", typeof(bool), typeof(SelectionCombo), new UIPropertyMetadata(true));

		public static readonly DependencyProperty CanExecuteProperty = CanExecutePropertyKey.DependencyProperty;


		#region Routed Events

		// Register the routed event for closing the DropDown
		public static readonly RoutedEvent DropDownClosedEvent =
			EventManager.RegisterRoutedEvent("DropDownClosed", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(SelectionCombo));

		/// <summary>
		/// Wrapper for the DropDownClosed Event
		/// </summary>
		public event RoutedEventHandler DropDownClosed
		{
			add { AddHandler(DropDownClosedEvent, value); }
			remove { RemoveHandler(DropDownClosedEvent, value); }
		}

		// Register the routed event for the SelectALL-Button
		public static readonly RoutedEvent SelectAllClickedEvent =
			EventManager.RegisterRoutedEvent("SelectAllClicked", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(SelectionCombo));

		/// <summary>
		/// Wrapper for the DropDownClosed Event
		/// </summary>
		public event RoutedEventHandler SelectAllClicked
		{
			add { AddHandler(SelectAllClickedEvent, value); }
			remove { RemoveHandler(SelectAllClickedEvent, value); }
		}

		#endregion

		public SelectionCombo()
		{
			InitializeComponent();
		}

		private void SelectAll_Click(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(SelectionCombo.SelectAllClickedEvent));
		}

		private void InternalComboBox_DropDownClosed(object sender, EventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(SelectionCombo.DropDownClosedEvent));
			if (Command != null && Command.CanExecute(this.CommandParameter))
				Command.Execute(this.CommandParameter);
		}

		private void TextBoxDisplayValues_KeyUp(object sender, KeyEventArgs e)
		{
			TextBox tb = sender as TextBox;
			if (tb != null && !String.IsNullOrEmpty(tb.Text) && e.Key != Key.Right && e.Key != Key.Left)
			{
				tb.SelectionStart = tb.Text.Length;
			}
		}

		private void InternalComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (!String.IsNullOrEmpty(this.SelectAllShortcut))
			{
				char shortcut = this.SelectAllShortcut[0];
				if (e.Key.ToString().ToLower().Equals(shortcut.ToString().ToLower()))
				{
					RaiseEvent(new RoutedEventArgs(SelectionCombo.SelectAllClickedEvent));
					this.SelectAllIsChecked = !this.SelectAllIsChecked;
				}
			}
		}

		private void TextBoxDisplayValues_LostFocus(object sender, RoutedEventArgs e)
		{
			if (Command != null && Command.CanExecute(this.CommandParameter))
				Command.Execute(this.CommandParameter);
		}
	}
}
