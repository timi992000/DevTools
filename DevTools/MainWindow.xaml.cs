using DevTools.ViewModels;
using MahApps.Metro.Controls;

namespace DevTools
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel(this);
		}
	}
}
