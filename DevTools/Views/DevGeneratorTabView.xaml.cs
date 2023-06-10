using DevTools.ViewModels;
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

namespace DevTools.Views
{
	/// <summary>
	/// Interaction logic for DevGeneratorTabView.xaml
	/// </summary>
	public partial class DevGeneratorTabView : UserControl
	{
		public DevGeneratorTabView()
		{
			InitializeComponent();
			DataContext = new DevGeneratorTabViewModel();
		}
	}
}
