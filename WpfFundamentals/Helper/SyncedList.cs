using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WpfFundamentals.ViewModelBase;

namespace WpfFundamentals.Helper
{
	/// <summary>
	/// Stellt eine Liste zur Verfügung, welche die Liste im Model mit dem im ViewModel synchronisiert. Dies geschieht nur in eine Richtung:
	/// Änderungen in der Liste im ViewModel werden in die Liste des Model geschrieben. Zur Verwendung im ViewModel stellt die SyncedList
	/// eine Observable Collection bereit.
	/// </summary>
	public class SyncedList<TModel, TViewModel>
	{
		ICollection<TModel> List { get; set; }
		protected Func<TModel, TViewModel> ViewModelCreator { get; private set; }
		Func<TViewModel, TModel> ModelGetter { get; set; }

		public ObservableCollection<TViewModel> ObservableCollection
		{
			get { return _observableCollection; }
		} ObservableCollection<TViewModel> _observableCollection;

		public SyncedList(ICollection<TModel> list, Func<TModel, TViewModel> viewModelCreator, Func<TViewModel, TModel> modelGetter)
		{
			this.ViewModelCreator = viewModelCreator;
			this.ModelGetter = modelGetter;
			_observableCollection = new ObservableCollection<TViewModel>();
			AdaptList(list);			
		}
		
		/// <summary>
		/// Für die aktuellen SyncedList wird für das Model eine neue Liste bereit gestellt. Diese wird automatisch mit der ObservableCollection bzw.
		/// der Liste im ViewModel abgeglichen. 
		/// </summary>
		public void AdaptList(ICollection<TModel> list)
		{
			this.ObservableCollection.CollectionChanged -= ObservableCollection_CollectionChanged;			

			this.List = list;
			ObservableCollection.Clear();

			var viewModels = from m in this.List select this.ViewModelCreator(m);

			foreach (var vm in viewModels)
				this.ObservableCollection.Add(vm);

			this.ObservableCollection.CollectionChanged += ObservableCollection_CollectionChanged;			
		}

		public void ReplaceAllItems(IEnumerable<TViewModel> viewModels)
		{
			ObservableCollection.Clear();
			foreach (TViewModel vm in viewModels)
				ObservableCollection.Add(vm);
		}

		public void Resync()
		{
			AdaptList(this.List);
		}

		void ObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					{
						AddItemToList(e);
						break;
					}
				case NotifyCollectionChangedAction.Remove:
					{
						RemoveItemFromList(e);
						break;
					}
				case NotifyCollectionChangedAction.Reset:
					{
						List.Clear();
						break;
					}
			}
		}

		void AddItemToList(NotifyCollectionChangedEventArgs e)
		{
			var models = from vm in e.NewItems.Cast<TViewModel>()
						 select this.ModelGetter(vm);

			foreach (var m in models)
				this.List.Add(m);
		}

		void RemoveItemFromList(NotifyCollectionChangedEventArgs e)
		{
			var models = from vm in e.OldItems.Cast<TViewModel>()
						 select this.ModelGetter(vm);
			foreach (var model in models)
				this.List.Remove(model);
		}
	}

	/// <summary>
	/// Stellt eine Liste zur Verfügung, welche die Liste im Model mit dem im ViewModel synchronisiert. Dies geschieht nur in eine Richtung:
	/// Änderungen in der Liste im ViewModel werden in die Liste des Model geschrieben. Zur Verwendung im ViewModel stellt die SyncedList
	/// eine Observable Collection bereit.
	/// </summary>
	public class SyncedViewModelList<TModel, TViewModel> : SyncedList<TModel, TViewModel>
		where TViewModel : ViewModel<TModel>
	{
		public List<TModel> Models
		{
			get
			{
				return this.ObservableCollection != null ? this.ObservableCollection.Models().ToList() : null;
			}
		}

		public SyncedViewModelList(ICollection<TModel> list, Func<TModel, TViewModel> viewModelCreator)
			: base(list, viewModelCreator, vm => vm.Model)
		{ }
	}
}
