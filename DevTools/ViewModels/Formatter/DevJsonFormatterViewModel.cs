using DevTools.Core.Extender;
using Newtonsoft.Json;
using System;
using System.IO;

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
					using (var stringReader = new StringReader(TextToFormat))
					using (var stringWriter = new StringWriter())
					{
						var jsonReader = new JsonTextReader(stringReader);
						var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
						jsonWriter.WriteToken(jsonReader);
						ConvertedText = stringWriter.ToString();
					}
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
