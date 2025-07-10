using System.Windows.Controls;
namespace WinSysTunerZ {
    public partial class HardwarePage : Page {
        public HardwarePage() {
            InitializeComponent();
            LoadHardwareInfo();
        }
        private void LoadHardwareInfo() {
            HardwareList.ItemsSource = Helpers.HardwareHelper.GetCpuInfo();
            // Optional: Erweiterbar mit RAM, Storage, GPU Infos
        }
    }
}