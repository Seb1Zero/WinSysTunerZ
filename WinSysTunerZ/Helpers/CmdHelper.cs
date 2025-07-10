using System.Diagnostics;
using System.Windows.Controls;
namespace WinSysTunerZ.Helpers {
    public static class CmdHelper
    {
        public static void RunCommand(string command, TextBox outputBox)
        {
            var psi = new ProcessStartInfo("cmd.exe", $"/c {command}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);
            if (proc != null)
            { // Ensure proc is not null before accessing its properties  
                outputBox.Text = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
            }
            else
            {
                outputBox.Text = "Fehler: Prozess konnte nicht gestartet werden.";
            }
        }
    }
}