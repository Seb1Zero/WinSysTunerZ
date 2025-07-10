using System;
using System.Linq;
using System.Windows;

namespace WinSysTunerZ.Helpers
{
    public static class ThemeManager
    {
        /// <summary>
        /// Lädt das aktuelle Theme und stellt sicher, dass Styles.xaml immer eingebunden ist.
        /// </summary>
        public static void Load()
        {
            try
            {
                string theme = Properties.Settings.Default.Theme;
                if (string.IsNullOrWhiteSpace(theme))
                    theme = "Dark"; // Fallback Default

                var stylesUri = new Uri("Themes/Styles.xaml", UriKind.Relative);
                var themeUri = new Uri($"Themes/{theme}.xaml", UriKind.Relative);

                var dicts = Application.Current.Resources.MergedDictionaries;

                // Extrahiere Styles.xaml falls bereits vorhanden
                var stylesDict = dicts.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Styles.xaml"));
                dicts.Clear();

                // Styles.xaml immer zuerst hinzufügen
                if (stylesDict == null)
                    dicts.Add(new ResourceDictionary() { Source = stylesUri });
                else
                    dicts.Add(stylesDict);

                // Theme hinzufügen
                dicts.Add(new ResourceDictionary() { Source = themeUri });
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
