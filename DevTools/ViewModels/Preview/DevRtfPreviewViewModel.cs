using DevTools.Core.Extender;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DevTools.ViewModels.Preview
{
	public class DevRtfPreviewViewModel : DevPreviewViewModelBase
	{
		public DevRtfPreviewViewModel()
			: base("rtf", @"{\rtf")
		{
		}

		public RichTextBox RtfTextBox
		{
			get => Get<RichTextBox>();
			set => Set(value);
		}

		public void TextChanged(string rtfContent)
		{
			if (RtfTextBox == null)
			{
				Execute_Open();
				return;
			}
			var documentBytes = Encoding.UTF8.GetBytes(rtfContent.GetRtfUnicodeEscapedString());
			using (var reader = new MemoryStream(documentBytes))
			{
				reader.Position = 0;
				RtfTextBox.SelectAll();
				RtfTextBox.Selection.Load(reader, DataFormats.Rtf);
			}
		}
	}
}
