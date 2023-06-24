using ControlzEx.Theming;
using DevTools.Core.BaseClasses;
using DevTools.Core.Configuration;
using DevTools.Core.Exceptions;
using DevTools.Core.Extender;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Windows;

namespace DevTools.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public static bool IsDarkMode { get; set; }
		public MainWindowViewModel(MetroWindow mainWindow)
		{
			MainWindow = mainWindow;
			__DetectWinThemeAndSetStart();
		}


		public static MetroWindow MainWindow { get; set; }

		public void Execute_SwitchTheme()
		{
			var currentTheme = ThemeManager.Current.DetectTheme();
			var inverseTheme = ThemeManager.Current.GetInverseTheme(currentTheme);
			ThemeManager.Current.ChangeTheme(Application.Current, inverseTheme);
		}
		private void __DetectWinThemeAndSetStart()
		{
			try
			{
                var DarkMode = DevToolsConfiguration.Instance.Get("DarkMode").ValidateAndRetrieveResult();
                var AccentColor = DevToolsConfiguration.Instance.Get("AccentColor").ValidateAndRetrieveResult();

                ThemeManager.Current.ChangeTheme(Application.Current, $"{(DarkMode.Equals("True") ? "Dark" : "Light")}.{AccentColor}");
				IsDarkMode = DarkMode.Equals("True");
            } 
			catch(OperationFailedException ex)
			{
				MessageBox.Show(ex.Message);
			} 
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				Environment.Exit(1);
			}
		}

	}
}
