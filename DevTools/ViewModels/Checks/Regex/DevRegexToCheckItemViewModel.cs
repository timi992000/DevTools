using ControlzEx.Theming;
using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DevTools.ViewModels.Checks.Regex
{
	public class DevRegexToCheckItemViewModel : ViewModelBase
	{
		private DevRegexCheckViewModel _ParentVM;
		public DevRegexToCheckItemViewModel(DevRegexCheckViewModel devRegexCheckViewModel)
		{
			_ParentVM = devRegexCheckViewModel;
			ThemeManager.Current.ThemeChanged +=
			(sender, e) =>
			{
				__SetRegexMatches();
			};
			__Init();
		}

		public string TempText;
		public string TextToCheck
		{
			get => Get<string>();
			set => Set(value.Replace("\r\n", string.Empty));
		}

		public RichTextBox ItemRichTextBox
		{
			get => Get<RichTextBox>();
			set => Set(value);
		}


		public void Execute_Remove()
		{
			_ParentVM.RemoveItem(this);
		}

		private void __Init()
		{
			_ParentVM.RegexExpressionChanged += (sender, e) =>
				{
					__SetRegexMatches();
				};
		}

		[DevDependsUpon(nameof(TextToCheck))]
		private void __SetRegexMatches()
		{
			try
			{
				if (_ParentVM.RegexExpression.IsNullOrEmpty() || !IsValidRegex(_ParentVM.RegexExpression) || TextToCheck.IsNullOrEmpty())
					return;
				var matches = System.Text.RegularExpressions.Regex.Matches(TextToCheck, _ParentVM.RegexExpression, RegexOptions.IgnoreCase);
				__ResetRichTextBoxWithCurrentText();
				if (matches.Any())
					__SetMatchingItems(matches);
			}
			catch (Exception)
			{
			}
		}

		private void __SetMatchingItems(MatchCollection matches)
		{
			try
			{
				matches.ToList().ForEach(match => __HighlightMatchingTexts(match.Value));
			}
			catch (Exception)
			{
			}
		}

		private void __ResetRichTextBoxWithCurrentText()
		{
			ExecuteWithoutDependendObjects(() =>
			{
				TempText = TextToCheck;
				SetRichTextBoxText();
			});
		}

		private void __HighlightMatchingTexts(string toHighlight)
		{
			try
			{
				if (ItemRichTextBox == null)
					return;

				TextRange range = new(ItemRichTextBox.Document.ContentStart, ItemRichTextBox.Document.ContentEnd);
				var reg = new System.Text.RegularExpressions.Regex(toHighlight, RegexOptions.Compiled | RegexOptions.IgnoreCase);

				var start = ItemRichTextBox.Document.ContentStart;
				while (start != null && start.CompareTo(ItemRichTextBox.Document.ContentEnd) < 0)
				{
					if (start.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
					{
						var match = reg.Match(start.GetTextInRun(LogicalDirection.Forward));
						var textrange = new TextRange(start.GetPositionAtOffset(match.Index, LogicalDirection.Forward), start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
						textrange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
						textrange.ApplyPropertyValue(TextElement.BackgroundProperty, MainWindowViewModel.IsDarkMode ? Brushes.ForestGreen : Brushes.LightGreen);
						start = textrange.End;
					}
					start = start.GetNextContextPosition(LogicalDirection.Forward);
				}
			}
			catch (Exception)
			{
			}
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

		public void SetRichTextBoxText()
		{
			if (ItemRichTextBox == null)
				return;
			ItemRichTextBox.Document.Blocks.Clear();
			if (TempText.IsNotNullOrEmpty())
				ItemRichTextBox.Document.Blocks.Add(new Paragraph(new Run(TempText)));
			TempText = string.Empty;
		}

		public void RefreshRegexMatching()
		{
			try
			{
				TextToCheck = new TextRange(ItemRichTextBox.Document.ContentStart, ItemRichTextBox.Document.ContentEnd).Text;
			}
			catch (Exception)
			{
			}
		}
	}
}
