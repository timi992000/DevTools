using System.Windows;

namespace DevTools.Core.BaseClasses
{
	public class DataBindingHelper : Freezable
	{
		public static readonly DependencyProperty DataProperty;

		static DataBindingHelper()
		{
			DataProperty = DependencyProperty.Register(nameof(Data), typeof(object), typeof(DataBindingHelper));
		}
		public object Data
		{
			get { return (object)GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		protected override Freezable CreateInstanceCore()
		{
			return new DataBindingHelper();
		}
	}
}
