using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WinSysTunerZ.Helpers
{
    public static class CmdHelper
    {
        public static void RunCommand(string command, TextBox outputBox)
        {
            Task.Run(() =>
            {
                var psi = new ProcessStartInfo("cmd.exe", $"/c {command}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                };

                var sb = new StringBuilder();
                using (var proc = new Process { StartInfo = psi })
                {
                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                        {
                            // WPF Controls müssen im UI-Thread geändert werden!
                            outputBox.Dispatcher.Invoke(() =>
                            {
                                outputBox.AppendText(e.Data + Environment.NewLine);
                                outputBox.ScrollToEnd();
                            });
                        }
                    };
                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                        {
                            outputBox.Dispatcher.Invoke(() =>
                            {
                                outputBox.AppendText("[ERR] " + e.Data + Environment.NewLine);
                                outputBox.ScrollToEnd();
                            });
                        }
                    };

                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                }
            });
        }
    }
}
