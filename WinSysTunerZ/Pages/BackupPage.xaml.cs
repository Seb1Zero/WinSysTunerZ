using System.Windows.Controls;
namespace WinSysTunerZ {
    public partial class BackupPage : Page
    {
        public BackupPage()
        {
            InitializeComponent();
            LoadBackups();
        }

        private void LoadBackups()
        {
            BackupList.ItemsSource = Helpers.BackupHelper.ListBackups();
        }

        private void CreateBackup_Click(object s, System.Windows.RoutedEventArgs e)
        {
            Helpers.BackupHelper.CreateBackup();
            LoadBackups();
        }

        private void DeleteBackup_Click(object s, System.Windows.RoutedEventArgs e)
        {
            if (BackupList.SelectedItem != null)
            {
                string? selectedBackup = BackupList.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedBackup))
                {
                    Helpers.BackupHelper.DeleteBackup(selectedBackup);
                }
            }
            LoadBackups();
        }
    }
}