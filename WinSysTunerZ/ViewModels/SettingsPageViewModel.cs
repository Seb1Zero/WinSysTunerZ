using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WinSysTunerZ.ViewModels
{
    public class ThemeEntry
    {
        public string Display { get; set; }
        public string Value { get; set; }
        public ThemeEntry(string display, string value)
        {
            Display = display;
            Value = value;
        }
        public override string ToString() => Display;
    }

    public class LanguageEntry
    {
        public string Display { get; set; }
        public string Value { get; set; }
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
        public ObservableCollection<ThemeEntry> Themes { get; set; } = new();
    }

    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        private const string ConfigPath = "settings.json";

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
                }
            }
        }

        public SettingsPageViewModel()
        {
            LoadConfig();

            if (Themes.Count == 0)
            {
                Themes.Add(new ThemeEntry("Light", "Light"));
                Themes.Add(new ThemeEntry("Dark", "Dark"));
                Themes.Add(new ThemeEntry("OLED", "OLED"));
            }
            if (SelectedTheme == null)
                SelectedTheme = Themes.FirstOrDefault(t => t.Value == "Dark") ?? Themes.FirstOrDefault();
            if (SelectedLanguage == null)
                SelectedLanguage = Languages.FirstOrDefault(l => l.Value == "de");
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
                        Themes = config.Themes ?? new ObservableCollection<ThemeEntry>();
                        _selectedTheme = Themes.FirstOrDefault(t => t.Value == config.SelectedTheme) ?? Themes.FirstOrDefault();
                        _selectedLanguage = Languages.FirstOrDefault(l => l.Value == config.SelectedLanguage) ?? Languages.FirstOrDefault();
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
                Themes = Themes
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
