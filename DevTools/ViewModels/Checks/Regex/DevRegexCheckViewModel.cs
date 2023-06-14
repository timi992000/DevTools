using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DevTools.ViewModels.Checks.Regex
{
	public class DevRegexCheckViewModel : ViewModelBase
	{
		public event EventHandler RegexExpressionChanged;
		public DevRegexCheckViewModel()
		{
			__Init();
		}

		public string RegexExpression
		{
			get => Get<string>();
			set
			{
				Set(value);
				RegexExpressionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public ObservableCollection<DevRegexToCheckItemViewModel> ItemsToCheck
		{
			get => Get<ObservableCollection<DevRegexToCheckItemViewModel>>();
			set => Set(value);
		}

		public void Execute_ImportClipBoardItem()
		{
			if (Clipboard.ContainsText())
			{
				__SetItemsToCheckFromString(Clipboard.GetText());

			}
		}

		private void __SetItemsToCheckFromString(string multiLineBaseString)
		{
			IEnumerable<DevRegexToCheckItemViewModel> collection = null;
			if (multiLineBaseString.IsNotNullOrEmpty())
			{
				collection = multiLineBaseString.Split("\r")
					.Select(u => u.Trim()).Where(s => s.IsNotNullOrEmpty() && !s.IsEquals("\n"))
					.Distinct().Select(n => __NewVM(n));
			}
			ItemsToCheck = new ObservableCollection<DevRegexToCheckItemViewModel>(collection);
			RegexExpressionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Execute_SetExample()
		{
			try
			{
				__SetItemsToCheckFromString(@"(123) 456-7890
(123)456-7890
123-456-7890
123.456.7890
1234567890
+31636363634
+91 (123) 456-7890
-91 (123)456-7890
+91 123-456-7890
+91 123.456.7890
+91 123456_7890
+91 123 456 7890");
				RegexExpression = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";
			}
			catch (Exception)
			{

			}
		}

		public void RemoveItem(DevRegexToCheckItemViewModel item)
		{
			ItemsToCheck.Remove(item);
			__AddEmptyItemIfNeeded();
		}

		private DevRegexToCheckItemViewModel __NewVM(string stringToCheck)
		{
			var vm = new DevRegexToCheckItemViewModel(this);
			vm.TextToCheck = stringToCheck;
			vm.PropertyChanged += (sender, e) =>
			{
				__AddEmptyItemIfNeeded();
			};
			return vm;
		}

		private void __Init()
		{
			ItemsToCheck = new ObservableCollection<DevRegexToCheckItemViewModel>();
			__AddEmptyItemIfNeeded();
			ItemsToCheck.CollectionChanged += (sender, e) =>
			{
				__AddEmptyItemIfNeeded();
			};
		}


		private void __AddEmptyItemIfNeeded()
		{
			try
			{
				if (ItemsToCheck.Count == 0 || ItemsToCheck.All(i => i.TextToCheck.IsNotNullOrEmpty()))
				{
					ItemsToCheck.Add(__NewVM(""));
					RegexExpressionChanged?.Invoke(this, EventArgs.Empty);
				}
			}
			catch (Exception)
			{
			}
		}

	}
}
