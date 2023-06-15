using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using QRCoder;
using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevTools.ViewModels.Generator
{
	public class DevQRCodeGeneratorViewModel : ViewModelBase
	{
		public DevQRCodeGeneratorViewModel()
		{
		}

		public string TextForGeneration
		{
			get => Get<string>();
			set => Set(value);
		}

		public ImageSource QRCode
		{
			get => Get<ImageSource>();
			set => Set(value);
		}

		[DevDependsUpon(nameof(TextForGeneration))]
		private void __Generate()
		{
			try
			{
				QRCodeGenerator qrGenerator = new QRCodeGenerator();
				QRCodeData qrCodeData = qrGenerator.CreateQrCode(TextForGeneration, QRCodeGenerator.ECCLevel.Q, forceUtf8: true, utf8BOM: true);
				QRCode qrCode = new QRCode(qrCodeData);
				__DrawImage(qrCode.GetGraphic(20));
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void __DrawImage(Bitmap bitmap)
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			ms.Position = 0;
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.StreamSource = ms;
			bi.EndInit();
			QRCode = bi;
		}

	}
}
