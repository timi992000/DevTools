using ControlzEx.Theming;
using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using System.Windows.Media;

namespace DevTools.ViewModels.Formatter
{
	public class DevFormatterViewModelBase : ViewModelBase
	{
		public DevFormatterViewModelBase()
		{
			ThemeManager.Current.ThemeChanged +=
				(sender, e) =>
				{
					OnPropertyChanged(nameof(Foreground));
				};
		}

		public string TextToFormat
		{
			get => Get<string>();
			set => Set(value);
		}

		public string ConvertedText
		{
			get => Get<string>();
			set => Set(value);
		}

		public bool HasError
		{
			get => Get<bool>();
			set => Set(value);
		}

		[DevDependsUpon(nameof(HasError))]
		public Brush Foreground
		{
			get
			{
				if (HasError)
					return Brushes.Red;
				return MainWindowViewModel.IsDarkMode ? Brushes.White : Brushes.Black;
			}
		}

		[DevDependsUpon(nameof(TextToFormat))]
		public virtual void FormatText()
		{

		}

	}
}
