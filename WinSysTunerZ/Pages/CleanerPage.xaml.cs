using System.Windows.Controls;
namespace WinSysTunerZ {
    public partial class CleanerPage : Page {
        public CleanerPage() {
            InitializeComponent();
        }
        private void CleanTemp_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.CleanTemp();
        private void CleanPrefetch_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.CleanPrefetch();
        private void CleanWUCache_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.CleanWindowsUpdateCache();
        private void EmptyRecycleBin_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.EmptyRecycleBin();
        private void RunDiskCleanup_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.RunDiskCleanup();
        private void CleanRegistryKeys_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CleanerHelper.CleanRegistryEmptyKeys();
    }
}