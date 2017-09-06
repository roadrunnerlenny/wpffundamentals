using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfFundamentals.ViewModelBase
{
	public interface IDelegateCommand : ICommand
	{
		void RaiseCanExecuteChanged();
	}

	public class ExecutingEventHandlerArgs<T> : EventArgs
	{
		public T Parameter { get; private set; }

		public ExecutingEventHandlerArgs(T parameter)
		{
			this.Parameter = parameter;
		}
	}

	public class ExecutedEventHandlerArgs<T> : EventArgs
	{
		public T Parameter { get; private set; }

		public ExecutedEventHandlerArgs(T parameter)
		{
			this.Parameter = parameter;
		}
	}

	/// <summary>
	/// Command-Mechanismus für ViewModel (einschließlich Presenter).
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DelegateCommand<T> : IDelegateCommand
	{
		Action<T> executeAction;
		Predicate<T> canExecuteAction;

		public event EventHandler<ExecutingEventHandlerArgs<T>> Executing = (s, e) => { };
		public event EventHandler<ExecutedEventHandlerArgs<T>> Executed = (s, e) => { };

		public DelegateCommand(Action<T> executeAction)
			: this(t => true, executeAction)
		{ }

		public DelegateCommand(Predicate<T> canExecuteAction, Action<T> executeAction)
		{
			this.canExecuteAction = canExecuteAction;
			this.executeAction = executeAction;
		}

		#region ICommand Member

		public bool CanExecute(object parameter)
		{
			return OnCanExecute((T)parameter);
		}

		public event EventHandler CanExecuteChanged = (s, e) => { };

		public void Execute(object parameter)
		{
			var typedParameter = (T)parameter;
			this.Executing(this, new ExecutingEventHandlerArgs<T>(typedParameter));
			this.OnExecute(typedParameter);
			this.Executed(this, new ExecutedEventHandlerArgs<T>(typedParameter));
		}

		#endregion

		public void RaiseCanExecuteChanged()
		{
			this.CanExecuteChanged(this, EventArgs.Empty);
		}

		public bool ExecuteIfPossible(T parameter)
		{
			bool canExecute = CanExecute(parameter);
			if (canExecute)
				Execute(parameter);
			return canExecute;
		}

		protected virtual bool OnCanExecute(T parameter)
		{
			if (this.canExecuteAction != null)
				return this.canExecuteAction(parameter);
			else
				return true;
		}

		protected virtual void OnExecute(T parameter)
		{			
			this.executeAction(parameter);
		}
	}

	public class DelegateCommand : DelegateCommand<object>
	{
		public DelegateCommand(Action executeAction)
			: base(parameter => executeAction())
		{
		}

		public DelegateCommand(Func<bool> canExecuteAction, Action executeAction)
			: base(parameter => canExecuteAction(), parameter => executeAction())
		{
		}

		public bool ExecuteIfPossible()
		{
			return base.ExecuteIfPossible(null);
		}
	}
}
