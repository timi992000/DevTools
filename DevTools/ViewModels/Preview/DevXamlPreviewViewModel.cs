using DevTools.Core.Attributes;
using DevTools.Core.Extender;
using DevTools.Core.Samples;
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DevTools.ViewModels.Preview
{
    public class DevXamlPreviewViewModel : DevPreviewViewModelBase
    {
        public DevXamlPreviewViewModel()
        {
            ContentControl = new ContentControl();
        }


        public ContentControl ContentControl
        {
            get => Get<ContentControl>();
            set
            {
                if (value == null)
                {
                    ContentControl = new ContentControl();
                    return;
                }
                Set(value);
            }
        }

        public string ErrorMessage
        {
            get => Get<string>();
            set => Set(value);
        }

        public void Execute_SetSample()
        {
            Text = TextSamples.XMLSampleText;
        }

        [DevDependsUpon(nameof(Text))]
        public void TextChanged()
        {
            var documentBytes = Encoding.UTF8.GetBytes(Text);
            using (var stream = new MemoryStream(documentBytes))
            {
                try
                {
                    ContentControl.Content = XamlReader.Load(stream);
                    ErrorMessage = null;
                }
                catch (Exception ex)
                {
                    if (Text.IsNullOrEmpty())
                    {
                        ErrorMessage = null;
                        ContentControl.Content = null;
                        return;
                    }
                    ErrorMessage = ex.InnerException.Message;
                }

            }
        }
    }
}
