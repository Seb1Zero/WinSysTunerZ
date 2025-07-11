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

                // Thread-Kulturen setzen
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;

                // Globale Default-Kultur setzen
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                // Ressourcen aktualisieren
                ResourceProvider.Instance.Refresh();
            }
            catch (CultureNotFoundException)
            {
                // Optional: Logging oder Fallback, aber KEINE MessageBox mehr
                // z.B.: Debug.WriteLine($"Sprache konnte nicht geladen werden: {ex.Message}");
            }
            catch (Exception)
            {
                // Optional: Logging oder Fallback, aber KEINE MessageBox mehr
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

                // Keine MessageBox.Show mehr!
            }
            catch (Exception)
            {
                // Optional: Logging, aber keine MessageBox mehr!
            }
        }
    }
}
