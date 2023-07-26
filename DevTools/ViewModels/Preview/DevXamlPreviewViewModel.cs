using DevTools.Core.Attributes;
using DevTools.Core.Extender;
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
            __SetSampleText();
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

        private void __SetSampleText()
        {
            Text = @"<UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">

    <Grid Margin=""10"" >
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width=""Auto""/>
            <ColumnDefinition Width=""Auto""/>
        </Grid.ColumnDefinitions>

        <Grid Grid.Column=""0"">
            <Grid.RowDefinitions>
                <RowDefinition Height=""Auto""/>
                <RowDefinition Height=""*""/>
            </Grid.RowDefinitions>

            <TextBlock Text=""Germany""
                       Grid.Row=""0""
											 FontSize=""20""/>

            <Grid Grid.Row=""1"">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width=""Auto""/>
                    <ColumnDefinition Width=""Auto""/>
                    <ColumnDefinition Width=""Auto""/>
                </Grid.ColumnDefinitions>

                <Rectangle Fill=""Black""
                       Height=""200""
                       Width=""100""
                       Grid.Column=""0""/>
                <Rectangle Fill=""Red""
                       Height=""200""
                       Width=""100""
                       Grid.Column=""1""/>
                <Rectangle Fill=""Yellow""
                       Height=""200""
                       Width=""100""
                       Grid.Column=""2""/>

            </Grid>

        </Grid>

        <Grid Grid.Column=""1"" Margin=""10 0 0 0"">
            <Grid.RowDefinitions>
                <RowDefinition Height=""Auto""/>
                <RowDefinition Height=""*""/>
                <RowDefinition Height=""*""/>
            </Grid.RowDefinitions>

            <TextBlock Text=""Ukraine""
                       Grid.Row=""0""
                       FontSize=""20""/>
            <Rectangle Fill=""Blue""
                       Height=""100""
                       Width=""300""
                       Grid.Row=""1""/>
            <Rectangle Fill=""Yellow""
                       Height=""100""
                       Width=""300""
                       Grid.Row=""2""/>

        </Grid>


    </Grid>
</UserControl>";
        }
    }
}
