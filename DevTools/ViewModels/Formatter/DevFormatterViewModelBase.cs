using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Windows.Media;
using System.Xml.Linq;

namespace DevTools.ViewModels.Formatter
{
	public class DevFormatterViewModelBase : ViewModelBase
	{
		public DevFormatterViewModelBase()
		{
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
		public Brush Foreground => HasError ? Brushes.Red : Brushes.White;

		[DevDependsUpon(nameof(TextToFormat))]
		public virtual void FormatText()
		{
		
		}

	}
}
