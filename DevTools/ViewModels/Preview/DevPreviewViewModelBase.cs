using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.ViewModels.Preview
{
	public class DevPreviewViewModelBase : ViewModelBase
	{
		private string _Extension;
		private string _StartCharacters;
		private bool _SkipStartChecking;

		public DevPreviewViewModelBase()
		{ }
		public DevPreviewViewModelBase(string extension, string startCharacters) : this(extension, startCharacters, false)
		{ }
		public DevPreviewViewModelBase(string extension, string startCharacters, bool skipStartChecking)
		{
			_Extension = extension;
			_StartCharacters = startCharacters;
			_SkipStartChecking = skipStartChecking;
		}

		public string Text
		{
			get => Get<string>() ?? "";
			set => Set(value);
		}

		public void Execute_Clear()
		{
			Text = null;
		}

		public void Execute_Open()
		{
			try
			{
				if (!_SkipStartChecking && !Text.ToStringValue().StartsWith(_StartCharacters))
					return;
				var Destination = Path.GetTempFileName();
				Destination = Path.ChangeExtension(Destination, _Extension);
				File.WriteAllText(Destination, Text);
				Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = Destination });
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex, MainWindowViewModel.MainWindow);
			}
		}
		
	}
}
