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

		}
	}
}
