using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace DevTools.ViewModels.Checks.Regex
{
	public class DevRegexToCheckItemViewModel : ViewModelBase
	{
		private DevRegexCheckViewModel _ParentVM;
		public DevRegexToCheckItemViewModel(DevRegexCheckViewModel devRegexCheckViewModel)
		{
			_ParentVM = devRegexCheckViewModel;
			__Init();
		}

		public string TextToCheck
		{
			get => Get<string>();
			set => Set(value);
		}

		public bool IsRegexMatch => _ParentVM.RegexExpression.IsNullOrEmpty() || !IsValidRegex(_ParentVM.RegexExpression) ? false : System.Text.RegularExpressions.Regex.IsMatch(TextToCheck, _ParentVM.RegexExpression, RegexOptions.IgnoreCase);

		[DevDependsUpon(nameof(TextToCheck))]
		public Brush IsMatchingBrush => IsRegexMatch ? Brushes.Green : Brushes.Transparent;


		public void Execute_Remove()
		{

		}

		private void __Init()
		{
			_ParentVM.RegexExpressionChanged += (sender, e) =>
				{
					OnPropertyChanged(nameof(IsMatchingBrush));
				};
		}

		private static bool IsValidRegex(string pattern)
		{
			if (pattern.IsNullOrEmpty()) return false;

			try
			{
				System.Text.RegularExpressions.Regex.Match("", pattern);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

	}
}
