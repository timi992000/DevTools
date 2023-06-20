using ControlzEx.Theming;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using MahApps.Metro.Controls;
using Microsoft.Win32;
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

		public bool DarkModeToggled
		{
			get => Get<bool>();
			set
			{
				Set(value);
				MainWindowViewModel.IsDarkMode = value;
			}
		}

		public void Execute_SwitchTheme()
		{
			var currentTheme = ThemeManager.Current.DetectTheme();
			var inverseTheme = ThemeManager.Current.GetInverseTheme(currentTheme);
			ThemeManager.Current.ChangeTheme(Application.Current, inverseTheme);
		}
		private void __DetectWinThemeAndSetStart()
		{
			var registryThemeKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
			if (registryThemeKey != null)
			{
				var useLightTheme = registryThemeKey.GetValue("SystemUsesLightTheme").ToStringValue() == "1";
				if (!useLightTheme)
					Execute_SwitchTheme();

				DarkModeToggled = !useLightTheme;
			}
		}

	}
}
