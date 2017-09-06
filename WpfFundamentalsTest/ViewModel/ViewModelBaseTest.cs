using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfFundamentals.ViewModelBase;

namespace WpfFundamentalsTest
{
	[TestClass]
	public class ViewModelBaseTest
	{

		public class Model
		{
			public string ModelProp { get; set; }
		}

		public class ViewModelWithModelImpl : ViewModel<Model>
		{
			public string ModelProp
			{
				get
				{
					return Model.ModelProp;
				}
				set
				{
					Model.ModelProp = value;
					OnPropertyChanged(() => this.ModelProp);
				}
			}

			public ViewModelWithModelImpl() :base(new Model())
			{ }

			public ViewModelWithModelImpl(Model model) :base(model)
			{ }
			
			public ViewModelWithModelImpl(ViewModelWithModelImpl other) :base(other)
			{ }
		}

		public class ViewModelImpl : ViewModel
		{ 
			public string TestProperty { get; set; }

			public const string TestPropertyName = "TestProperty";

			public void TestOnPropertyChange() {
				this.OnPropertyChanged(()=>this.TestProperty);
			}
		}

		public ViewModelImpl VMImpl { get; set; }

		public ViewModelWithModelImpl VMWithModelImpl { get; set; }

		public bool WasPropChangeEventCalled { get; set; }

		[TestMethod]
		public void TestPropertyNameVerification()
		{
			VMImpl = new ViewModelImpl();

			VMImpl.PropertyChanged += VMImpl_PropertyChanged;

			WasPropChangeEventCalled = false;
			VMImpl.TestOnPropertyChange();
			Assert.IsTrue(WasPropChangeEventCalled); 
		}

		void VMImpl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{			
			Assert.AreEqual(e.PropertyName, ViewModelImpl.TestPropertyName);
			Assert.AreEqual(sender, VMImpl);
			WasPropChangeEventCalled = true;
		}

		[TestMethod]
		public void TestCopyModel()
		{
			string testPropName = "Test";
			VMWithModelImpl = new ViewModelWithModelImpl();
			VMWithModelImpl.ModelProp = testPropName ;
			ViewModelWithModelImpl Copy = new ViewModelWithModelImpl(VMWithModelImpl);
			Assert.AreEqual(VMWithModelImpl.ModelProp, testPropName);
			Assert.AreEqual(VMWithModelImpl.Model.ModelProp, testPropName );
			Assert.AreEqual(Copy.ModelProp, testPropName);
			Assert.AreEqual(Copy.Model.ModelProp, testPropName);
		}
	}
}
