using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Text;
using System.Windows;

namespace DevTools.ViewModels.Generator
{
	public class DevGuidGeneratorViewModel : ViewModelBase
	{
		public DevGuidGeneratorViewModel()
		{
			RequestCount = 1;
			Hyphens = true;
		}

		public int RequestCount
		{
			get => Get<int>();
			set => Set(value);
		}

		public bool Uppercase
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool Braces
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool Hyphens
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool Base64
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool RFC7515
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool URLEncode
		{
			get => Get<bool>();
			set => Set(value);
		}

		public string GeneratedGuidText
		{
			get => Get<string>();
			set => Set(value);
		}

		public void Execute_Generate()
		{
			try
			{
				__GenerateGuid();
			}
			catch (Exception)
			{

			}
		}

		public void Execute_Copy()
		{
			try
			{
				if (GeneratedGuidText.IsNotNullOrEmpty())
					Clipboard.SetText(GeneratedGuidText);
			}
			catch (Exception)
			{

			}
		}

		private void __GenerateGuid()
		{
			string completedGuidString = "";
			for (int i = 0; i < RequestCount; i++)
			{
				var baseGuid = Guid.NewGuid().ToStringValue();
				if (Braces)
					baseGuid = "{" + baseGuid + "}";
				if (!Hyphens)
					baseGuid = baseGuid.Replace("-", string.Empty);
				if (Uppercase)
					baseGuid = baseGuid.ToUpper();
				if (Base64)
					baseGuid = Convert.ToBase64String(Encoding.UTF8.GetBytes(baseGuid), Base64FormattingOptions.InsertLineBreaks);

				completedGuidString += $"{baseGuid}\n";
			}
			GeneratedGuidText = completedGuidString;
		}
	}
}
