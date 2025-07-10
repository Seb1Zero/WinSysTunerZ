using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace WinSysTunerZ.Helpers
{
    /// <summary>
    /// Globale ResourceProvider-Klasse für Mehrsprachigkeit.
    /// Bindings z.B. über {x:Static res:ResourceProvider.Instance} im XAML.
    /// </summary>
    public class ResourceProvider : INotifyPropertyChanged
    {
        // Singleton-Instanz (thread-safe)
        private static readonly ResourceProvider _instance = new();
        public static ResourceProvider Instance => _instance;

        // ResourceManager als readonly
        private readonly ResourceManager _resourceManager = new("WinSysTunerZ.Resources.Resource", typeof(ResourceProvider).Assembly);

        // Event nullable (CS8612)
        public event PropertyChangedEventHandler? PropertyChanged;

        // Privater Konstruktor
        private ResourceProvider()
        {
            // Kein try-catch nötig hier, da Field initialisiert wird (keine CS8618 Warnung mehr)
        }

        // Indexer für Sprachstrings
        public string this[string key]
        {
            get
            {
                try
                {
                    return _resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture) ?? $"[{key}]";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Resource key '{key}' error: {ex.Message}");
                    return $"[{key}]";
                }
            }
        }

        /// <summary>
        /// Triggert PropertyChanged für alle Bindings (nach Sprachwechsel).
        /// </summary>
        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Sprache global ändern und Refresh triggern
        /// </summary>
        public void ChangeCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Refresh();
        }
    }
}
