using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfFundamentals.ViewModelBase
{
	public interface IPresenter
	{
		object View { get; }		
	}

	public class Presenter<TView> : ViewModel, IPresenter where TView : class, new()
	{
		public TView View { get; private set; }

		public Presenter()
		{
			this.View = new TView();			
		}

		#region IPresenterBase Member

		object IPresenter.View
		{
			get { return this.View; }
		}		

		#endregion
	}
}
