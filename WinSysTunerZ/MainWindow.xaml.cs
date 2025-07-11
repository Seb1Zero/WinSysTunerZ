using System.Windows;
using System.Windows.Input;
using WinSysTunerZ.Pages;
using WinSysTunerZ.ViewModels;

namespace WinSysTunerZ
{
    public partial class MainWindow : Window
    {
        private readonly SettingsPageViewModel _appViewModel;
        private readonly SettingsPage _settingsPage;

        public MainWindow()
        {
            InitializeComponent();

            _appViewModel = new SettingsPageViewModel();
            this.DataContext = _appViewModel;

            // SettingsPage bekommt das ViewModel als DataContext
            _settingsPage = new SettingsPage { DataContext = _appViewModel };

            // Startseite setzen
            MainFrame.Navigate(new RegistryPage());
        }

        // Navigations-Methoden für Sidebar
        private void Nav_Registry(object sender, RoutedEventArgs e) => MainFrame.Navigate(new RegistryPage());
        private void Nav_Cmd(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CmdToolsPage());
        private void Nav_Cleaner(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CleanerPage());
        private void Nav_Backup(object sender, RoutedEventArgs e) => MainFrame.Navigate(new BackupPage());
        private void Nav_Hardware(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HardwarePage());
        private void Nav_Settings(object sender, RoutedEventArgs e) => MainFrame.Navigate(_settingsPage); // Korrigiert!

        // Rest wie gehabt ...
    }
}
