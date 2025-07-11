using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WinSysTunerZ.Helpers
{
    public static class ThemeManager
    {
        /// <summary>
        /// Laedt das aktuelle Theme – bevorzugt aus dem User-Profil, sonst aus App-Verzeichnis.
        /// </summary>
        public static void Load()
        {
            try
            {
                string theme = Properties.Settings.Default.Theme;
                if (string.IsNullOrWhiteSpace(theme))
                    theme = "Dark"; // Fallback Default

                var stylesUri = new Uri("Themes/Styles.xaml", UriKind.Relative);

                // User-Ordner fuer Custom Themes
                string userThemePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "WinSysTunerZ", "Themes", theme + ".xaml");

                ResourceDictionary themeDict;
                if (File.Exists(userThemePath))
                {
                    // Aus User-Ordner laden (Dateipfad → Absolute URI)
                    themeDict = new ResourceDictionary() { Source = new Uri(userThemePath, UriKind.Absolute) };
                }
                else
                {
                    // Aus dem Installationsverzeichnis (Themes-Ordner im Output)
                    themeDict = new ResourceDictionary() { Source = new Uri($"Themes/{theme}.xaml", UriKind.Relative) };
                }

                var dicts = Application.Current.Resources.MergedDictionaries;
                var stylesDict = dicts.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Styles.xaml"));
                dicts.Clear();

                // Styles.xaml immer zuerst einbinden
                if (stylesDict == null)
                    dicts.Add(new ResourceDictionary() { Source = stylesUri });
                else
                    dicts.Add(stylesDict);

                dicts.Add(themeDict);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Theme konnte nicht geladen werden: {ex.Message}", "Theme Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Setzt und speichert das neue Theme.
        /// </summary>
        public static void Set(string theme)
        {
            try
            {
                Properties.Settings.Default.Theme = theme;
                Properties.Settings.Default.Save();
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Setzen des Themes: {ex.Message}", "Theme Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
