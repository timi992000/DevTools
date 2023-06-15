using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using QRCoder;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevTools.ViewModels.Generator
{
  public class DevQRCodeGeneratorViewModel : ViewModelBase
  {
    public DevQRCodeGeneratorViewModel()
    {
      ScalingFactor = 1;
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

    public double ScalingFactor
    {
      get => Get<double>();
      set => Set(value);
    }

    public void Execute_CopyQRCode()
    {
      try
      {
        Clipboard.SetImage(QRCode as BitmapImage);
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
      }
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
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
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

    public void ChangeScalingFactor(int delta)
    {
      if (ScalingFactor > 0.09 && ScalingFactor < 0.2 && delta < 0)
        return;
      double step = 0.1;
      if (delta > 0)
        ScalingFactor += step;
      else
        ScalingFactor -= step;
    }

    public void KeyDown(KeyEventArgs e)
    {
      if (Keyboard.Modifiers == ModifierKeys.Control)
      {
        if (e.Key == Key.D0)
          ScalingFactor = 1;
        else if (e.Key == Key.OemMinus)
          ChangeScalingFactor(-1);
        else if (e.Key == Key.OemPlus)
          ChangeScalingFactor(1);
      }
    }
  }
}
