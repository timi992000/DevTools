using DevTools.Core.Attributes;
using DevTools.Core.Extender;
using DevTools.Core.Samples;
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

        [DevDependsUpon(nameof(Text))]
        public void TextChanged()
        {
            if (RtfTextBox == null)
            {
                Execute_Open();
                return;
            }
            try
            {
                if (Text.IsNullOrEmpty())
                {
                    RtfTextBox.SelectAll();
                    RtfTextBox.Document.Blocks.Clear();
                    return;
                }
                var documentBytes = Encoding.UTF8.GetBytes(Text);
                using (var reader = new MemoryStream(documentBytes))
                {
                    reader.Position = 0;
                    RtfTextBox.SelectAll();
                    RtfTextBox.Selection.Load(reader, DataFormats.Rtf);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Execute_SetSample()
        {
            Text = null;
            Text = TextSamples.RtfSampleText;
        }

    }
}
