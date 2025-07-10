using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
namespace WinSysTunerZ.Helpers {
    public static class UpdateManager
    {
        private const string LatestUrl = "https://raw.githubusercontent.com/USERNAME/REPO/main/latest.txt";
        private const string DownloadUrl = "https://github.com/USERNAME/REPO/releases/latest/download/WinSysTunerZ.exe";

        public static async Task CheckForUpdate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var current = assembly?.GetName().Version?.ToString() ?? string.Empty; // Null-check added
            using var client = new HttpClient();
            var latest = (await client.GetStringAsync(LatestUrl)).Trim();
            if (latest != current)
            {
                var tmp = Path.Combine(Path.GetTempPath(), "WinSysTunerZ_Update.exe");
                var data = await client.GetByteArrayAsync(DownloadUrl);
                await File.WriteAllBytesAsync(tmp, data);
                Process.Start(new ProcessStartInfo(tmp) { UseShellExecute = true });
                Environment.Exit(0);
            }
        }
    }
}