using DevTools.Core.Extender;
using DevTools.ViewModels.Preview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevTools.Views.Preview
{
	/// <summary>
	/// Interaction logic for DevXamlPreviewView.xaml
	/// </summary>
	public partial class DevXamlPreviewView : UserControl
	{
		public DevXamlPreviewView()
		{
			InitializeComponent();
			DataContext = new DevXamlPreviewViewModel();
			_ViewModel.ContentControl = Content;
		}


		public DevXamlPreviewViewModel _ViewModel => DataContext is DevXamlPreviewViewModel ?
			DataContext as DevXamlPreviewViewModel : new DevXamlPreviewViewModel();

		private void __TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb)
				_ViewModel.Text = tb.Text.ToStringValue();
		}

	}
}
