using System;
using System.Windows;

namespace WinSysTunerZ
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // 🔷 Theme laden
                Helpers.ThemeManager.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Theme konnte nicht geladen werden:\n{ex.Message}");
            }

            try
            {
                // 🔷 Sprache laden
                Helpers.LanguageManager.Apply();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sprache konnte nicht geladen werden:\n{ex.Message}");
            }
        }
    }
}
