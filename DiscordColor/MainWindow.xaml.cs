using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiscordColor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public GridData GData = new GridData();

		public MainWindow()
		{
			InitializeComponent();

			MainGrid.DataContext = GData;
		}

		private void LightenColor(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				if (button.Name == "LightenOne" && Helpers.IsHex(GData.Color1))
					GData.Color1 = Helpers.LightenColor(GData.Color1);
				else if(button.Name == "LightenTwo" && Helpers.IsHex(GData.Color2))
					GData.Color2 = Helpers.LightenColor(GData.Color2);
				else if (button.Name == "LightenThree" && Helpers.IsHex(GData.Color3))
					GData.Color3 = Helpers.LightenColor(GData.Color3);
			}
		}

		private void DarkenColor(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				if (button.Name == "DarkenOne" && Helpers.IsHex(GData.Color1))
					GData.Color1 = Helpers.DarkenColor(GData.Color1);
				else if (button.Name == "DarkenTwo" && Helpers.IsHex(GData.Color2))
					GData.Color2 = Helpers.DarkenColor(GData.Color2);
				else if (button.Name == "DarkenThree" && Helpers.IsHex(GData.Color3))
					GData.Color3 = Helpers.DarkenColor(GData.Color3);
			}
		}

		private void SaturateClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				if (button.Name == "VibrateOne" && Helpers.IsHex(GData.Color1))
					GData.Color1 = Helpers.SaturateColor(GData.Color1);
				else if (button.Name == "VibrateTwo" && Helpers.IsHex(GData.Color2))
					GData.Color2 = Helpers.SaturateColor(GData.Color2);
				else if (button.Name == "VibrateThree" && Helpers.IsHex(GData.Color3))
					GData.Color3 = Helpers.SaturateColor(GData.Color3);
			}
		}

		private void DullClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				if (button.Name == "DullOne" && Helpers.IsHex(GData.Color1))
					GData.Color1 = Helpers.DullColor(GData.Color1);
				else if (button.Name == "DullTwo" && Helpers.IsHex(GData.Color2))
					GData.Color2 = Helpers.DullColor(GData.Color2);
				else if (button.Name == "DullThree" && Helpers.IsHex(GData.Color3))
					GData.Color3 = Helpers.DullColor(GData.Color3);
			}
		}
	}

	public class GridData : INotifyPropertyChanged
	{
		//private string _loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
		private string _loremIpsum = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789";
		public string LoremIpsum
		{
			get
			{
				return _loremIpsum;
			}
		}

		public string DarkBackground
		{
			get
			{
				return "#36393e";
			}
		}

		public string LightBackground
		{
			get
			{
				return "#FFFFFF";
			}
		}

		private string _color1 = "#000000";
		public string Color1
		{
			get
			{
				return _color1;
			}
			set
			{
				_color1 = value;
				NotifyPropertyChanged("Color1");
				NotifyPropertyChanged("OneLightContrast");
				NotifyPropertyChanged("OneDarkContrast");
			}
		}

		private string _color2 = "#000000";
		public string Color2
		{
			get
			{
				return _color2;
			}
			set
			{
				_color2 = value;
				NotifyPropertyChanged("Color2");
				NotifyPropertyChanged("TwoLightContrast");
				NotifyPropertyChanged("TwoDarkContrast");
			}
		}

		private string _color3 = "#000000";
		public string Color3
		{
			get
			{
				return _color3;
			}
			set
			{
				_color3 = value;
				NotifyPropertyChanged("Color3");
				NotifyPropertyChanged("ThreeLightContrast");
				NotifyPropertyChanged("ThreeDarkContrast");
			}
		}

		public double OneLightContrast
		{
			get
			{
				return Helpers.GetContrast(LightBackground, Color1);
			}
		}

		public double TwoLightContrast
		{
			get
			{
				return Helpers.GetContrast(LightBackground, Color2);
			}
		}

		public double ThreeLightContrast
		{
			get
			{
				return Helpers.GetContrast(LightBackground, Color3);
			}
		}

		public double OneDarkContrast
		{
			get
			{
				return Helpers.GetContrast(DarkBackground, Color1);
			}
		}

		public double TwoDarkContrast
		{
			get
			{
				return Helpers.GetContrast(DarkBackground, Color2);
			}
		}

		public double ThreeDarkContrast
		{
			get
			{
				return Helpers.GetContrast(DarkBackground, Color3);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
		}
	}
}
