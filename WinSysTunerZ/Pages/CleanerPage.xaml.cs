using System.Windows.Controls;
namespace WinSysTunerZ {
    public partial class CleanerPage : Page {
        public CleanerPage() {
            InitializeComponent();
        }
        private async void CleanTemp_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.CleanTempAsync(OutputBox);
        private async void CleanPrefetch_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.CleanPrefetchAsync(OutputBox);
        private async void CleanWUCache_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.CleanWindowsUpdateCacheAsync(OutputBox);
        private async void EmptyRecycleBin_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.EmptyRecycleBinAsync(OutputBox);
        private async void RunDiskCleanup_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.RunDiskCleanupAsync(OutputBox);
        private async void CleanRegistryKeys_Click(object s, System.Windows.RoutedEventArgs e) => await Helpers.CleanerHelper.CleanRegistryEmptyKeysAsync(OutputBox);
    }
}