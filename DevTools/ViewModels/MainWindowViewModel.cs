using ControlzEx.Theming;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Threading;
using System.Windows;

namespace DevTools.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
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
			var registryThemeKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
			if (registryThemeKey != null)
			{
				var useLightTheme = registryThemeKey.GetValue("SystemUsesLightTheme").ToStringValue() == "1";
				if (!useLightTheme)
					Execute_SwitchTheme();
			}

			//for (int i = 0; i < 10; i++)
			//{
					//Inverse the windows theme every x Seconds
			//	registryThemeKey.SetValue("SystemUsesLightTheme",i % 2 == 0 ? "1" : "0", RegistryValueKind.DWord);

			//	Thread.Sleep(1000);
			//}

		}

	}
}
