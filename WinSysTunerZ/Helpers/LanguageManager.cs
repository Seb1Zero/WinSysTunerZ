using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace WinSysTunerZ.Helpers
{
    public static class LanguageManager
    {
        /// <summary>
        /// Wendet die aktuell gespeicherte Sprache an (z.B. bei App-Start).
        /// </summary>
        public static void Apply()
        {
            try
            {
                string lang = Properties.Settings.Default.Language;
                if (string.IsNullOrWhiteSpace(lang))
                    lang = "de"; // Fallback Default

                var culture = new CultureInfo(lang);

                // Thread-kulture setzen
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;

                // Globale Default-Kultur setzen (optional, falls Threads dynamisch erzeugt werden)
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                // Ressourcen aktualisieren
                ResourceProvider.Instance.Refresh();
            }
            catch (CultureNotFoundException ex)
            {
                MessageBox.Show($"Sprache konnte nicht geladen werden: {ex.Message}", "Language Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unbekannter Fehler bei Sprache: {ex.Message}", "Language Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Setzt und speichert die neue Sprache.
        /// </summary>
        public static void Set(string lang)
        {
            try
            {
                Properties.Settings.Default.Language = lang;
                Properties.Settings.Default.Save();
                Apply();

                // Benutzer informieren (optional)
                MessageBox.Show($"Sprache gewechselt zu: {lang}", "Sprache geändert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Wechseln der Sprache: {ex.Message}", "Language Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
