using DevTools.Core.Extender;
using Newtonsoft.Json;
using System;

namespace DevTools.ViewModels.Formatter
{
	public class DevJsonFormatterViewModel : DevFormatterViewModelBase
	{
		public override void FormatText()
		{
			try
			{
				if (TextToFormat.IsNullOrEmpty())
					ConvertedText = string.Empty;
				else
				{
					dynamic parsedJson = JsonConvert.DeserializeObject(TextToFormat);
					ConvertedText = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
				}
				HasError = false;
			}
			catch (Exception ex)
			{
				HasError = false;
				HasError = true;
				ConvertedText = (ex.InnerException ?? ex).ToStringValue();
			}
		}

	}
}
