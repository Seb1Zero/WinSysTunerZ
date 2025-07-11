using System.Windows;
using WinSysTunerZ.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WinSysTunerZ
{
    public partial class SettingsPage : System.Windows.Controls.Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        // Theme hinzufügen
        private void AddTheme_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog();
            if (dialog.ShowDialog() == true)
            {
                string name = dialog.ThemeName.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Bitte einen Namen für das Theme angeben.", "Fehler");
                    return;
                }
                string pHex = $"#{dialog.PrimaryColor.R:X2}{dialog.PrimaryColor.G:X2}{dialog.PrimaryColor.B:X2}";
                string sHex = $"#{dialog.SecondaryColor.R:X2}{dialog.SecondaryColor.G:X2}{dialog.SecondaryColor.B:X2}";
                var colors = new Dictionary<string, string>
                {
                    { "PrimaryColor", pHex },
                    { "SecondaryColor", sHex },
                };

                Helpers.ThemeGenerator.Generate(name, colors);

                if (DataContext is SettingsPageViewModel vm)
                {
                    if (vm.Themes.Any(t => t.Display == name))
                    {
                        MessageBox.Show("Theme mit diesem Namen existiert bereits.", "Info");
                        return;
                    }
                    vm.AddTheme(name, name);
                    vm.SelectedTheme = vm.Themes.Last();
                }
                MessageBox.Show($"Theme '{name}' erfolgreich erstellt!", "Theme", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Theme entfernen
        private void RemoveTheme_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsPageViewModel vm && vm.SelectedTheme != null)
            {
                if (MessageBox.Show(
                    $"Theme '{vm.SelectedTheme.Display}' wirklich löschen?",
                    "Theme entfernen",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                ) == MessageBoxResult.Yes)
                {
                    vm.RemoveTheme(vm.SelectedTheme);
                }
            }
        }

        // Theme gewechselt
        private void ThemeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataContext is SettingsPageViewModel vm && vm.SelectedTheme != null)
            {
                Helpers.ThemeManager.Set(vm.SelectedTheme.Value);
                vm.SaveConfig();
            }
        }

        // Sprache gewechselt
        private void LanguageComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataContext is SettingsPageViewModel vm && vm.SelectedLanguage != null)
            {
                Helpers.LanguageManager.Set(vm.SelectedLanguage.Value);
                vm.SaveConfig();
            }
        }
    }
}
