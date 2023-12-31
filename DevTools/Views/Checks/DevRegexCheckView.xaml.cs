﻿using DevTools.ViewModels.Checks.Regex;
using System.Windows.Controls;

namespace DevTools.Views.Checks
{
	/// <summary>
	/// Interaction logic for DevRegexCheckView.xaml
	/// </summary>
	public partial class DevRegexCheckView : UserControl
	{
		public DevRegexCheckView()
		{
			InitializeComponent();
			DataContext = new DevRegexCheckViewModel();
		}

		private void __TextChanged(object sender, TextChangedEventArgs e)
		{
			if (DataContext is DevRegexCheckViewModel vm && sender is TextBox tb)
				vm.RegexExpression = tb.Text;
		}

		private void __ItemToCheckChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is RichTextBox tb && tb.DataContext is DevRegexToCheckItemViewModel vm)
				vm.RefreshRegexMatching();
		}

		private void __Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (sender is RichTextBox tb && tb.DataContext is DevRegexToCheckItemViewModel vm)
			{
				vm.ItemRichTextBox = tb;
				vm.SetRichTextBoxText();
			}
		}
	}
}
