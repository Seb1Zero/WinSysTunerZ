using System.Windows;
using System.Windows.Media;

namespace WinSysTunerZ
{
    public partial class ColorPickerDialog : Window
    {
        public string ThemeName => ThemeNameBox.Text.Trim();
        public Color PrimaryColor { get; private set; } = Colors.MediumPurple;
        public Color SecondaryColor { get; private set; } = Colors.MediumSlateBlue;

        public ColorPickerDialog(string? themeName = null, Color? primary = null, Color? secondary = null)
        {
            InitializeComponent();
            if (themeName != null) ThemeNameBox.Text = themeName;
            PrimaryColorPicker.SelectedColor = primary ?? PrimaryColor;
            SecondaryColorPicker.SelectedColor = secondary ?? SecondaryColor;
        }

        private void PrimaryColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
                PrimaryColor = e.NewValue.Value;
        }

        private void SecondaryColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
                SecondaryColor = e.NewValue.Value;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ThemeName))
            {
                MessageBox.Show("Bitte einen Namen für das Theme angeben.");
                return;
            }
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
