using System;
using System.Collections.Generic;
using System.IO;

namespace WinSysTunerZ.Helpers
{
    public static class ThemeGenerator
    {
        private static readonly string[] ButtonStyleLines =
        {
            "  <Style TargetType=\"Button\">",
            "    <Setter Property=\"Foreground\" Value=\"{StaticResource ButtonForegroundBrush}\" />",
            "    <Setter Property=\"Background\" Value=\"{StaticResource ButtonBackgroundBrush}\" />",
            "    <Setter Property=\"Padding\" Value=\"10,5\"/>",
            "    <Setter Property=\"Margin\" Value=\"5\"/>",
            "    <Setter Property=\"FontSize\" Value=\"14\"/>",
            "    <Setter Property=\"BorderThickness\" Value=\"0\"/>",
            "    <Setter Property=\"Cursor\" Value=\"Hand\"/>",
            "    <Setter Property=\"Template\">",
            "      <Setter.Value>",
            "        <ControlTemplate TargetType=\"Button\">",
            "          <Border x:Name=\"border\"",
            "                  Background=\"{TemplateBinding Background}\"",
            "                  CornerRadius=\"8\"",
            "                  SnapsToDevicePixels=\"True\">",
            "            <ContentPresenter HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>",
            "          </Border>",
            "          <ControlTemplate.Triggers>",
            "            <Trigger Property=\"IsMouseOver\" Value=\"True\">",
            "              <Setter TargetName=\"border\" Property=\"Background\" Value=\"{StaticResource ButtonHoverBrush}\"/>",
            "            </Trigger>",
            "            <Trigger Property=\"IsPressed\" Value=\"True\">",
            "              <Setter TargetName=\"border\" Property=\"Background\" Value=\"{StaticResource ButtonPressedBrush}\"/>",
            "            </Trigger>",
            "            <Trigger Property=\"IsEnabled\" Value=\"False\">",
            "              <Setter TargetName=\"border\" Property=\"Opacity\" Value=\"0.5\"/>",
            "            </Trigger>",
            "          </ControlTemplate.Triggers>",
            "        </ControlTemplate>",
            "      </Setter.Value>",
            "    </Setter>",
            "  </Style>",
            ""
        };

        /// <summary>
        /// Generiert eine vollständige Theme XAML-Datei inkl. Colors, Brushes und Button Style.
        /// </summary>
        /// <param name="name">Dateiname ohne .xaml</param>
        /// <param name="colors">Dictionary mit Schlüssel=ColorKey, Wert=HexColor</param>
        public static void Generate(string name, Dictionary<string, string> colors)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Theme name darf nicht leer sein.");

                if (colors == null || colors.Count == 0)
                    throw new ArgumentException("Colors Dictionary darf nicht leer sein.");

                var lines = new List<string>
                {
                    "<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"",
                    "                    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">",
                    ""
                };

                // Colors
                foreach (var kv in colors)
                {
                    lines.Add($"  <Color x:Key=\"{kv.Key}\">{kv.Value}</Color>");
                }

                lines.Add("");

                // Brushes
                foreach (var kv in colors)
                {
                    lines.Add($"  <SolidColorBrush x:Key=\"{kv.Key.Replace("Color", "Brush")}\" Color=\"{{StaticResource {kv.Key}}}\" />");
                }

                lines.Add("");

                // Button Style
                lines.AddRange(ButtonStyleLines);

                lines.Add("</ResourceDictionary>");

                // Sicherstellen, dass der Themes-Ordner existiert
                string themesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Themes");
                Directory.CreateDirectory(themesDir);

                string filePath = Path.Combine(themesDir, $"{name}.xaml");
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"Theme '{name}' erfolgreich generiert unter {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ThemeGenerator] Fehler beim Generieren des Themes: {ex.Message}");
            }
        }
    }
}
