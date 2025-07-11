using System;
using System.Collections.Generic;
using System.IO;

namespace WinSysTunerZ.Helpers
{
    public static class ThemeGenerator
    {
        // ... ButtonStyleLines bleibt unverändert

        private static readonly string[] ButtonStyleLines =
        {
            // ... (dein Code unverändert)
        };

        /// <summary>
        /// Generiert eine vollstaendige Theme XAML-Datei inkl. Colors, Brushes und Button Style.
        /// Speichert im User-Ordner (%APPDATA%\WinSysTunerZ\Themes).
        /// </summary>
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

                // User-Ordner als Speicherort nutzen!
                string userThemesDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "WinSysTunerZ", "Themes");
                Directory.CreateDirectory(userThemesDir);

                string filePath = Path.Combine(userThemesDir, $"{name}.xaml");
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
