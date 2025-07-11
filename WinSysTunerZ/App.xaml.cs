using System;
using System.Threading.Tasks;
using System.Windows;
using Velopack;

namespace WinSysTunerZ
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Theme laden
            try
            {
                Helpers.ThemeManager.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Theme konnte nicht geladen werden:\n{ex.Message}");
            }

            // Sprache laden
            try
            {
                Helpers.LanguageManager.Apply();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sprache konnte nicht geladen werden:\n{ex.Message}");
            }

            // Autoupdate via Velopack
            await CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            // Hier muss der direkte RAW-URL zu deinem Update-Feed stehen!
            string updateUrl = "https://github.com/Seb1Zero/WinSysTunerZ/releases/latest/download/";

            try
            {
                var mgr = new UpdateManager(updateUrl);

                // Nach neuer Version suchen
                var newVersion = await mgr.CheckForUpdatesAsync();
                if (newVersion == null)
                    return; // Kein Update verfügbar

                // Update herunterladen
                await mgr.DownloadUpdatesAsync(newVersion);

                // Anwenden und Neustart
                mgr.ApplyUpdatesAndRestart(newVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Update-Check:\n{ex.Message}");
            }
        }
    }
}
