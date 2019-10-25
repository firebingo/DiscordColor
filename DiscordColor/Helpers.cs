using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace DiscordColor
{
	public static class Helpers
	{
		public static bool IsHex(string i)
		{
			if (i.Length == 7)
				return Regex.IsMatch(i, "#[a-fA-F0-9]{6}");
			else if (i.Length == 4)
				return Regex.IsMatch(i, "#[a-fA-F0-9]{3}");
			else
				return false;
		}

		//https://www.w3.org/TR/2008/REC-WCAG20-20081211/#contrast-ratiodef
		public static double GetContrast(string foreground, string background)
		{
			if (!IsHex(foreground) || !IsHex(background))
				return -1.0;
			Color c1 = ColorTranslator.FromHtml(foreground);
			Color c2 = ColorTranslator.FromHtml(background);
			var l1 = GetLuminance(c1);
			var l2 = GetLuminance(c2);
			if(l1 > l2)
				return (l1 + 0.05) / (l2 + 0.05);
			else
				return (l2 + 0.05) / (l1 + 0.05);
		}

		//https://www.w3.org/TR/2008/REC-WCAG20-20081211/#relativeluminancedef
		private static double GetLuminance(Color color)
		{
			double[] a = new double[3] { color.R, color.G, color.B };
			for(var i = 0; i < 3; ++i)
			{
				var v = a[i] / 255;
				a[i] = v  < 0.03928 ? v / 12.92 : Math.Pow((v + 0.055) / 1.055, 2.4);
			}
			return 0.2126 * a[0] + 0.7152 * a[1] + 0.0722 * a[2];
		}

		public static string LightenColor(string hex)
		{
			if (!IsHex(hex))
				return hex;
			Color c = ColorTranslator.FromHtml(hex);
			c = Color.FromArgb(Math.Min(255, c.R + 1), Math.Min(255, c.G + 1), Math.Min(255, c.B + 1));
			return ColorTranslator.ToHtml(c);
		}

		public static string DarkenColor(string hex)
		{
			if (!IsHex(hex))
				return hex;
			Color c = ColorTranslator.FromHtml(hex);
			c = Color.FromArgb(Math.Max(0, c.R - 1), Math.Max(0, c.G - 1), Math.Max(0, c.B - 1));
			return ColorTranslator.ToHtml(c);
		}

		public static string SaturateColor(string hex)
		{
			if (!IsHex(hex))
				return hex;
			Color c = ColorTranslator.FromHtml(hex);
			var hsv = HSVColor.ColorToHSV(c);
			var newSat = Math.Min(1.0, hsv.s + 0.05);
			c = HSVColor.ColorFromHSV(hsv.h, newSat, hsv.v);
			return ColorTranslator.ToHtml(c);
		}

		public static string DullColor(string hex)
		{
			if (!IsHex(hex))
				return hex;
			Color c = ColorTranslator.FromHtml(hex);
			var hsv = HSVColor.ColorToHSV(c);
			var newSat = Math.Max(0, hsv.s - 0.05);
			c = HSVColor.ColorFromHSV(hsv.h, newSat, hsv.v);
			return ColorTranslator.ToHtml(c);
		}
	}

	public struct HSVColor
	{
		public double h { get; }
		public double s { get; }
		public double v { get; }

		public HSVColor(double h, double s, double v)
		{
			this.h = h;
			this.s = s;
			this.v = v;
		}

		//https://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
		public static HSVColor ColorToHSV(Color color)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			double hue = color.GetHue();
			double saturation = (max == 0) ? 0 : 1.0 - (1.0 * min / max);
			double value = max / 255.0;
			return new HSVColor(hue, saturation, value);
		}

		public static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return Color.FromArgb(255, v, t, p);
			else if (hi == 1)
				return Color.FromArgb(255, q, v, p);
			else if (hi == 2)
				return Color.FromArgb(255, p, v, t);
			else if (hi == 3)
				return Color.FromArgb(255, p, q, v);
			else if (hi == 4)
				return Color.FromArgb(255, t, p, v);
			else
				return Color.FromArgb(255, v, p, q);
		}
	}
}
