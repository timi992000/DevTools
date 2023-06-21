using DevTools.Core.Commands;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevTools.Controls
{
	/// <summary>
	/// Interaction logic for SwappableContentPanel.xaml
	/// </summary>
	public partial class SwappableContentPanel : UserControl
	{
		private RelayCommand _SwapCommand;
		public event EventHandler Swapped;

		public SwappableContentPanel()
		{
			InitializeComponent();
		}


		#region ContentPanel1
		public static object GetContentPanel1(DependencyObject obj)
		{
			return (object)obj.GetValue(ContentPanel1Property);
		}

		public static void SetContentPanel1(DependencyObject obj, object value)
		{
			obj.SetValue(ContentPanel1Property, value);
		}

		// Using a DependencyProperty as the backing store for ContentPanel1.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentPanel1Property =
				DependencyProperty.RegisterAttached("ContentPanel1", typeof(object), typeof(SwappableContentPanel), new PropertyMetadata(null, __ContentPanel1Changed));

		private static void __ContentPanel1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion

		#region ContentPanel2
		public static object GetContentPanel2(DependencyObject obj)
		{
			return (object)obj.GetValue(ContentPanel2Property);
		}

		public static void SetContentPanel2(DependencyObject obj, object value)
		{
			obj.SetValue(ContentPanel2Property, value);
		}

		// Using a DependencyProperty as the backing store for ContentPanel2.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentPanel2Property =
				DependencyProperty.RegisterAttached("ContentPanel2", typeof(object), typeof(SwappableContentPanel), new PropertyMetadata(null, __ContentPanel2Changed));

		private static void __ContentPanel2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion

		#region SwapMode
		public static eSwapMode GetSwapMode(DependencyObject obj)
		{
			return (eSwapMode)obj.GetValue(SwapModeProperty);
		}

		public static void SetSwapMode(DependencyObject obj, eSwapMode value)
		{
			obj.SetValue(SwapModeProperty, value);
		}

		// Using a DependencyProperty as the backing store for SwapMode.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SwapModeProperty =
				DependencyProperty.Register("SwapMode", typeof(eSwapMode), typeof(SwappableContentPanel), new PropertyMetadata(eSwapMode.Horizontal, __SwapModeChanged));

		public RelayCommand SwapCommand
		{
			get
			{
				if (_SwapCommand == null)
				{
					_SwapCommand = new RelayCommand(p => __SwapPanels());
				}
				return _SwapCommand;
			}
		}

		private static void __SwapModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}
		#endregion

		#region FirstPanelBorderStyle
		public static Style GetFirstPanelBorderStyle(DependencyObject obj)
		{
			return (Style)obj.GetValue(FirstPanelBorderStyleProperty);
		}

		public static void SetFirstPanelBorderStyle(DependencyObject obj, Style value)
		{
			obj.SetValue(FirstPanelBorderStyleProperty, value);
		}

		// Using a DependencyProperty as the backing store for FirstPanelBorderStyle.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FirstPanelBorderStyleProperty =
				DependencyProperty.RegisterAttached("FirstPanelBorderStyle", typeof(Style), typeof(SwappableContentPanel), new PropertyMetadata(null, __FirstPanelBorderStyleChanged));

		private static void __FirstPanelBorderStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is SwappableContentPanel swappableContentPanel && e.NewValue is Style stl)
			{
				if (!swappableContentPanel.IsLoaded)
				{
					swappableContentPanel.Loaded += (sender, e) =>
					{
						swappableContentPanel.__UpdateFirstPanelBorderStyle(stl);
					};
				}
				else
					swappableContentPanel.__UpdateFirstPanelBorderStyle(stl);
			}
		}
		#endregion

		#region SecondPanelBorderStyle
		public static Style GetSecondPanelBorderStyle(DependencyObject obj)
		{
			return (Style)obj.GetValue(SecondPanelBorderStyleProperty);
		}

		public static void SetSecondPanelBorderStyle(DependencyObject obj, Style value)
		{
			obj.SetValue(SecondPanelBorderStyleProperty, value);
		}

		// Using a DependencyProperty as the backing store for SecondPanelBorderStyle.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SecondPanelBorderStyleProperty =
				DependencyProperty.RegisterAttached("SecondPanelBorderStyle", typeof(Style), typeof(SwappableContentPanel), new PropertyMetadata(null, __SecondPanelBorderStyleChanged));

		private static void __SecondPanelBorderStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is SwappableContentPanel swappableContentPanel && e.NewValue is Style stl)
			{
				if (!swappableContentPanel.IsLoaded)
				{
					swappableContentPanel.Loaded += (sender, e) =>
					{
						swappableContentPanel.__UpdateSecondPanelBorderStyle(stl);
					};
				}
				else
					swappableContentPanel.__UpdateSecondPanelBorderStyle(stl);
			}
		}
		#endregion

		private void __SwapPanels()
		{
			if (GetSwapMode(this) == eSwapMode.Horizontal)
			{
				var leftSide = this.FindChild<Border>("ContentBorderHorizontalLeft");
				var rightSide = this.FindChild<Border>("ContentBorderHorizontalRight");

				if (leftSide != null && rightSide != null)
				{
					var leftColumn = Grid.GetColumn(leftSide);
					if (leftColumn == 0)
					{
						Grid.SetColumn(leftSide, 2);
						Grid.SetColumn(rightSide, 0);
					}
					else
					{
						Grid.SetColumn(leftSide, 0);
						Grid.SetColumn(rightSide, 2);
					}
				}
			}
			else
			{
				var topSide = this.FindChild<Border>("ContentBorderVerticalTop");
				var bottomSide = this.FindChild<Border>("ContentBorderVerticalBottom");

				if (topSide != null && bottomSide != null)
				{
					var leftColumn = Grid.GetRow(topSide);
					if (leftColumn == 0)
					{
						Grid.SetRow(topSide, 2);
						Grid.SetRow(bottomSide, 0);
					}
					else
					{
						Grid.SetRow(topSide, 0);
						Grid.SetRow(bottomSide, 2);
					}
				}
			}
			Swapped?.Invoke(this, EventArgs.Empty);
		}

		private void __UpdateFirstPanelBorderStyle(Style style)
		{
			if (GetSwapMode(this) == eSwapMode.Horizontal)
			{
				var leftSide = this.FindChild<Border>("ContentBorderHorizontalLeft");
				if (leftSide != null)
					leftSide.Style = style;
			}
			else
			{
				var topSide = this.FindChild<Border>("ContentBorderVerticalTop");

				if (topSide != null)
					topSide.Style = style;
			}
		}

		private void __UpdateSecondPanelBorderStyle(Style style)
		{
			if (GetSwapMode(this) == eSwapMode.Horizontal)
			{
				var rightSide = this.FindChild<Border>("ContentBorderHorizontalRight");

				if (rightSide != null)
					rightSide.Style = style;
			}
			else
			{
				var bottomSide = this.FindChild<Border>("ContentBorderVerticalBottom");

				if (bottomSide != null)
					bottomSide.Style = style;
			}
		}
	}

	public enum eSwapMode
	{
		Horizontal,
		Vertical,
	}

}
