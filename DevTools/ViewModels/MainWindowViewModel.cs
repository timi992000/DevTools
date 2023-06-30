using ControlzEx.Theming;
using DevTools.Core.BaseClasses;
using DevTools.Core.Configuration;
using DevTools.Core.Exceptions;
using DevTools.Core.Extender;
using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace DevTools.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private const string DarkModeUserConfigKey = "DarkMode";

    public static bool IsDarkMode { get; set; }
    public MainWindowViewModel(MetroWindow mainWindow)
    {
      MainWindow = mainWindow;
      __DetectWinThemeAndSetStart();
    }


    public static MetroWindow MainWindow { get; set; }

    public bool ConfigurationOpen
    {
      get => Get<bool>();
      set => Set(value);
    }

    public void Execute_SwitchTheme()
    {
      var currentTheme = ThemeManager.Current.DetectTheme();
      var inverseTheme = ThemeManager.Current.GetInverseTheme(currentTheme);
      ThemeManager.Current.ChangeTheme(Application.Current, inverseTheme);
      DevToolsConfiguration.Instance.Set(DarkModeUserConfigKey, IsDarkMode ? "True" : "False");
    }
    private void __DetectWinThemeAndSetStart()
    {
      try
      {
        var DarkMode = DevToolsConfiguration.Instance.Get(DarkModeUserConfigKey).ValidateAndRetrieveResult();
        var AccentColor = DevToolsConfiguration.Instance.Get("AccentColor").ValidateAndRetrieveResult();
        ThemeManager.Current.ChangeTheme(Application.Current, $"{(DarkMode.Equals("True") ? "Dark" : "Light")}.{AccentColor}");
        IsDarkMode = DarkMode.Equals("True");
      }
      catch (OperationFailedException ex)
      {
        ShowErrorMessage(ex);
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
        Environment.Exit(1);
      }
    }

  }
}
