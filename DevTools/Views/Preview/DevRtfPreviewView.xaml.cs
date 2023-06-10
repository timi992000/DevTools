using DevTools.Core.Extender;
using DevTools.ViewModels.Preview;
using System.Windows.Controls;

namespace DevTools.Views.Preview
{
	/// <summary>
	/// Interaction logic for DevRtfPreviewView.xaml
	/// </summary>
	public partial class DevRtfPreviewView : UserControl
	{
		public DevRtfPreviewView()
		{
			InitializeComponent();
			DataContext = new DevRtfPreviewViewModel();
			_ViewModel.RtfTextBox = RtfTextBox;
		}

		public DevRtfPreviewViewModel _ViewModel => DataContext is DevRtfPreviewViewModel ?
			DataContext as DevRtfPreviewViewModel : new DevRtfPreviewViewModel();

		private void __TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb)
				_ViewModel.TextChanged(tb.Text.ToStringValue());
		}
	}
}
