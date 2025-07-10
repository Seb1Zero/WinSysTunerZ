using System.Windows.Controls;
namespace WinSysTunerZ {
    public partial class CmdToolsPage : Page {
        public CmdToolsPage() {
            InitializeComponent();
        }
        private void RunSFC_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CmdHelper.RunCommand("sfc /scannow", OutputBox);
        private void RunDISM_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CmdHelper.RunCommand("dism /online /cleanup-image /scanhealth", OutputBox);
        private void RunIPConfig_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CmdHelper.RunCommand("ipconfig /all", OutputBox);
        private void RunPing_Click(object s, System.Windows.RoutedEventArgs e) => Helpers.CmdHelper.RunCommand("ping google.com", OutputBox);
    }
}