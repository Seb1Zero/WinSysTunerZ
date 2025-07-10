using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WinSysTunerZ.Models;
using WinSysTunerZ.Helpers;
using System.Runtime.Versioning;

namespace WinSysTunerZ.Pages
{
    public partial class RegistryPage : Page
    {
        // Sichtbare Einstellungen im UI
        public ObservableCollection<RegistrySetting> Settings { get; }

        // Commands zum Anwenden / Zurücksetzen
        public ICommand ApplyCommand { get; }
        public ICommand ResetCommand { get; }

        public RegistryPage()
        {
            InitializeComponent();

            // 1) Default-Definitionen laden
            var defaults = RegistrySetting.GetDefaults(); // Du musst GetDefaults() implementieren!

            // 2) Aktuellen Wert aus Registry auslesen (nur auf Windows-Plattformen)
            if (OperatingSystem.IsWindows())
            {
                RefreshCurrentValues(defaults);
            }

            Settings = new ObservableCollection<RegistrySetting>(defaults);

            // 3) Commands binden
            ApplyCommand = new RelayCommand<RegistrySetting>(s =>
            {
                if (OperatingSystem.IsWindows())
                {
                    s?.ApplyDefault();
                }
            });
            ResetCommand = new RelayCommand<RegistrySetting>(s =>
            {
                if (OperatingSystem.IsWindows())
                {
                    s?.ResetToDefault();
                }
            });

            // 4) ViewModel als DataContext
            DataContext = this;
        }

        [SupportedOSPlatform("windows")]
        private void RefreshCurrentValues(IEnumerable<RegistrySetting> settings)
        {
            foreach (var setting in settings)
            {
                setting.RefreshCurrent();
            }
        }
    }
}
