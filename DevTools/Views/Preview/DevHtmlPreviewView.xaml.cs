using DevTools.Core.Extender;
using DevTools.ViewModels.Preview;
using System.Windows.Controls;

namespace DevTools.Views.Preview
{
	/// <summary>
	/// Interaction logic for DevHtmlPreview.xaml
	/// </summary>
	public partial class DevHtmlPreview : UserControl
	{
		public DevHtmlPreview()
		{
			InitializeComponent();
			DataContext = new DevHtmlPreviewViewModel();
			_ViewModel.BrowserControl = webview;
		}

		private DevHtmlPreviewViewModel _ViewModel => DataContext is DevHtmlPreviewViewModel ?
			DataContext as DevHtmlPreviewViewModel : new DevHtmlPreviewViewModel();

		private void __TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb)
				_ViewModel.NavigateToString(tb.Text.ToStringValue());
		}
	}
}
