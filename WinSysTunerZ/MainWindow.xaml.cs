using System.Windows;
using System.Windows.Input;
using WinSysTunerZ.Pages; // Hinzugefügt: Namespace für Seiten

namespace WinSysTunerZ
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new RegistryPage());
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
            else
                DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Nav_Registry(object sender, RoutedEventArgs e) => MainFrame.Navigate(new RegistryPage());
        private void Nav_Cmd(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CmdToolsPage());
        private void Nav_Cleaner(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CleanerPage());
        private void Nav_Backup(object sender, RoutedEventArgs e) => MainFrame.Navigate(new BackupPage());
        private void Nav_Hardware(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HardwarePage());
        private void Nav_Settings(object sender, RoutedEventArgs e) => MainFrame.Navigate(new SettingsPage());
    }
}
