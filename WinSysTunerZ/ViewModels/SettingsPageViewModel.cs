using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Windows; // Für MessageBox

namespace WinSysTunerZ.ViewModels
{
    public class ThemeEntry
    {
        public string Display { get; set; } = null!;
        public string Value { get; set; } = null!;

        public ThemeEntry() { } // Für JSON-Serialisierung!
        public ThemeEntry(string display, string value)
        {
            Display = display;
            Value = value;
        }
        public override string ToString() => Display;
    }

    public class LanguageEntry
    {
        public string Display { get; set; } = null!;
        public string Value { get; set; } = null!;

        public LanguageEntry() { }
        public LanguageEntry(string display, string value)
        {
            Display = display;
            Value = value;
        }
        public override string ToString() => Display;
    }

    public class SettingsConfig
    {
        public string? SelectedTheme { get; set; }
        public string? SelectedLanguage { get; set; }
        public List<ThemeEntry> Themes { get; set; } = new();
    }

    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        private const string ConfigPath = "settings.json";
        private bool _isInitializing = true; // Flag für Initialisierung

        public ObservableCollection<ThemeEntry> Themes { get; set; } = new();
        public ObservableCollection<LanguageEntry> Languages { get; set; } = new()
        {
            new LanguageEntry("Deutsch", "de"),
            new LanguageEntry("Englisch", "en")
        };

        private ThemeEntry? _selectedTheme;
        public ThemeEntry? SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value && value != null)
                {
                    _selectedTheme = value;
                    OnPropertyChanged(nameof(SelectedTheme));
                    SaveConfig();
                }
            }
        }

        private LanguageEntry? _selectedLanguage;
        public LanguageEntry? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value && value != null)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                    SaveConfig();

                    // Automatischer Neustart nur bei aktiver Änderung durch den User!
                    if (!_isInitializing)
                    {
                        MessageBox.Show($"Die Sprache wurde geändert. Die App wird jetzt neu gestartet.", "Sprache geändert", MessageBoxButton.OK, MessageBoxImage.Information);

                        // App automatisch neu starten 
                        var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
                        if (!string.IsNullOrEmpty(exePath))
                            System.Diagnostics.Process.Start(exePath);
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        // === App-Metadaten als Properties ===

        public string AppTitle => GetAppTitle();
        public string AppVersion => GetAppVersion();
        public string AppAuthor => GetAppAuthor();

        private string GetAppTitle()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>();
            return attr?.Product ?? "WinSysTunerZ";
        }

        private string GetAppVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return version != null ? $"v{version}" : "v?";
        }

        private string GetAppAuthor()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>();
            if (attr != null && !string.IsNullOrWhiteSpace(attr.Company))
                return attr.Company;
            var authorsAttr = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>();
            if (authorsAttr != null && !string.IsNullOrWhiteSpace(authorsAttr.Copyright))
                return authorsAttr.Copyright;
            return "Unbekannt";
        }

        public SettingsPageViewModel()
        {
            // Immer zuerst Themes und Languages befüllen (damit Binding klappt)
            if (Themes.Count == 0)
            {
                Themes.Add(new ThemeEntry("Light", "Light"));
                Themes.Add(new ThemeEntry("Dark", "Dark"));
                Themes.Add(new ThemeEntry("OLED", "OLED"));
            }
            LoadConfig();
            if (SelectedTheme == null)
                SelectedTheme = Themes.FirstOrDefault(t => t.Value == "Dark") ?? Themes.FirstOrDefault();
            if (SelectedLanguage == null)
                SelectedLanguage = Languages.FirstOrDefault(l => l.Value == "en"); // Standard jetzt auf Englisch!

            _isInitializing = false; // Ab jetzt sind User-Aktionen möglich
        }

        public void AddTheme(string display, string value)
        {
            Themes.Add(new ThemeEntry(display, value));
            SelectedTheme = Themes.Last();
            SaveConfig();
        }

        public void RemoveTheme(ThemeEntry? theme)
        {
            if (theme != null && Themes.Contains(theme) && Themes.Count > 1)
            {
                if (SelectedTheme == theme)
                    SelectedTheme = Themes.FirstOrDefault(t => t != theme);
                Themes.Remove(theme);
                SaveConfig();
            }
        }

        public void LoadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    var json = File.ReadAllText(ConfigPath);
                    var config = JsonSerializer.Deserialize<SettingsConfig>(json);

                    if (config != null)
                    {
                        Themes.Clear();
                        foreach (var t in config.Themes ?? new List<ThemeEntry>())
                            Themes.Add(t);

                        var selTheme = Themes.FirstOrDefault(t => t.Value == config.SelectedTheme);
                        if (selTheme != null) SelectedTheme = selTheme;

                        var selLang = Languages.FirstOrDefault(l => l.Value == config.SelectedLanguage);
                        if (selLang != null) SelectedLanguage = selLang;
                    }
                }
                catch { /* ignore & fallback to defaults */ }
            }
        }

        public void SaveConfig()
        {
            var config = new SettingsConfig
            {
                SelectedTheme = SelectedTheme?.Value,
                SelectedLanguage = SelectedLanguage?.Value,
                Themes = Themes.ToList()
            };
            try
            {
                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch { /* ignore for now */ }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
