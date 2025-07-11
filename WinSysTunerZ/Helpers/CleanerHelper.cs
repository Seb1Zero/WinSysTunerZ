using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WinSysTunerZ.Helpers
{
    public static class OutputHelper
    {
        public static void ShowProgress(TextBox outputBox, string functionName)
        {
            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText($"{functionName} in Progress, don't close the app...\n");
                outputBox.ScrollToEnd();
            });
        }
    }

    public static class CleanerHelper
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

        public static async Task EmptyRecycleBinAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Papierkorb leeren");

            await Task.Run(() =>
            {
                const uint SHERB_NOCONFIRMATION = 0x00000001;
                const uint SHERB_NOPROGRESSUI = 0x00000002;
                const uint SHERB_NOSOUND = 0x00000004;

                SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
            });

            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText("Papierkorb wurde geleert.\n");
                outputBox.ScrollToEnd();
            });
        }

        public static async Task CleanTempAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Temp-Ordner bereinigen");
            int deleted = 0;
            string tempPath = Path.GetTempPath();

            await Task.Run(() =>
            {
                try
                {
                    foreach (var file in Directory.EnumerateFiles(tempPath, "*", SearchOption.AllDirectories))
                    {
                        try { File.Delete(file); deleted++; } catch { }
                    }
                    foreach (var dir in Directory.EnumerateDirectories(tempPath, "*", SearchOption.AllDirectories))
                    {
                        try { Directory.Delete(dir, true); } catch { }
                    }
                }
                catch { /* ignore */ }
            });

            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText($"Temp-Ordner bereinigt. {deleted} Dateien gelöscht.\n");
                outputBox.ScrollToEnd();
            });
        }

        public static async Task CleanPrefetchAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Prefetch-Ordner bereinigen");
            string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
            int deleted = 0;
            await Task.Run(() =>
            {
                try
                {
                    foreach (var file in Directory.GetFiles(prefetchPath, "*.pf"))
                    {
                        try { File.Delete(file); deleted++; } catch { }
                    }
                }
                catch { /* ignore */ }
            });

            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText($"Prefetch-Ordner bereinigt. {deleted} Dateien gelöscht.\n");
                outputBox.ScrollToEnd();
            });
        }

        public static async Task CleanWindowsUpdateCacheAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Windows Update-Cache bereinigen");
            string updatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "SoftwareDistribution", "Download");
            int deleted = 0;

            await Task.Run(() =>
            {
                try
                {
                    foreach (var file in Directory.GetFiles(updatePath))
                    {
                        try { File.Delete(file); deleted++; } catch { }
                    }
                    foreach (var dir in Directory.GetDirectories(updatePath))
                    {
                        try { Directory.Delete(dir, true); } catch { }
                    }
                }
                catch { /* ignore */ }
            });

            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText($"Windows Update-Cache bereinigt. {deleted} Dateien gelöscht.\n");
                outputBox.ScrollToEnd();
            });
        }

        public static async Task RunDiskCleanupAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Datenträgerbereinigung");
            await Task.Run(() =>
            {
                Process.Start("cleanmgr.exe");
            });
            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText("Datenträgerbereinigung wurde gestartet.\n");
                outputBox.ScrollToEnd();
            });
        }

        public static async Task CleanRegistryEmptyKeysAsync(TextBox outputBox)
        {
            OutputHelper.ShowProgress(outputBox, "Leere Registry-Schlüssel bereinigen");
            // Achtung: Leere Registry-Schlüssel zu löschen ist nicht ganz trivial und erfordert meist Admin-Rechte!
            // Hier nur eine Platzhalter-Logik (kann mit konkretem Registry-Scan ergänzt werden)
            await Task.Delay(500); // Dummy-Operation
            outputBox.Dispatcher.Invoke(() =>
            {
                outputBox.AppendText("Leere Registry-Schlüssel wurden bereinigt (Platzhalter).\n");
                outputBox.ScrollToEnd();
            });
        }
    }
}
