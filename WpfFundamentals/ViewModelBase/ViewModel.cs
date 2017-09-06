using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml;
using WpfFundamentals.Helper;

namespace WpfFundamentals.ViewModelBase
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Member

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public void AllPropertiesChanged()
		{
			if (this.PropertyChanged != null)
			{
				PropertyInfo[] pi = this.GetType().GetProperties();
				foreach (PropertyInfo prop in pi)
					this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
			}
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
		{
			VerifyProperty(expression);
			string propertyName = Reflect.GetPropertyName(expression);
			if (PropertyChanged != null)
			    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		[System.Diagnostics.Conditional("DEBUG")]
		void VerifyProperty<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException("propertyExpression");

			var memberExpression = propertyExpression.Body as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");

			var property = memberExpression.Member as System.Reflection.PropertyInfo;
			if (property == null)
				throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");

			if (!property.DeclaringType.IsAssignableFrom(this.GetType()))
				throw new ArgumentException("The referenced property belongs to a different type.", "propertyExpression");

			var getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				// this shouldn't happen - the expression would reject the property before reaching this far
				throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
			}

			if (getMethod.IsStatic)
				throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
		}

		protected void SetAllPropertiesToDefaultValue()
		{
			PropertyInfo[] pi = this.GetType().GetProperties();
			foreach (PropertyInfo prop in pi)
			{
				object[] atts = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);
				if (atts != null && atts.Length > 0)
				{
					DefaultValueAttribute att = (DefaultValueAttribute)atts[0];
					prop.SetValue(this, att.Value, null);
				}
			}
		}
	}

	public class ViewModel<T> : ViewModel
    {
        public T Model { get; private set; }

        public ViewModel(T model)
        {
            this.Model = model;
        }

        public ViewModel(ViewModel<T> other) : this(CopyModel(other.Model)) 
		{ }		

        public T CopyModel()
        {
            return CopyModel(this.Model);
        }

		public void AdaptModel(T model)
		{
			this.Model = model;
		}

		static readonly DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

		public static T CopyModel(T model)
		{
			try
			{
				using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
				{
					using (var writer = XmlDictionaryWriter.CreateBinaryWriter(stream))
					{
						dataContractSerializer.WriteObject(writer, model);
						writer.Flush();
						stream.Position = 0;

						using (var reader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
						{
							T modelCopy = (T)dataContractSerializer.ReadObject(reader);
							return modelCopy;
						}
					}
				}
			}
			catch (Exception e)
			{
				throw new SerializationException("Das Model-Objekt kann nicht über den XML-Serializer serialisiert werden", e);
			}
		}
	}

}
