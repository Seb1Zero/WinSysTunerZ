using System;
using System.Diagnostics;
using System.IO;
namespace WinSysTunerZ.Helpers {
    public static class BackupHelper {
        private static string BackupFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WinSysTunerZ_Backups");
        public static void CreateBackup() {
            Directory.CreateDirectory(BackupFolder);
            var file = Path.Combine(BackupFolder, $"RegBackup_{DateTime.Now:yyyyMMdd_HHmmss}.reg");
            Process.Start(new ProcessStartInfo("reg", $"export HKLM {file} /y") { UseShellExecute = false });
        }
        public static string[] ListBackups() => Directory.Exists(BackupFolder) ? Directory.GetFiles(BackupFolder, "*.reg") : Array.Empty<string>();
        public static void DeleteBackup(string path) { if(File.Exists(path)) File.Delete(path); }
    }
}