using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices; // Hinzugefügt, um DllImport zu verwenden
using Microsoft.Win32;

namespace WinSysTunerZ.Helpers {
    public static class CleanerHelper
    {
        [DllImport("shell32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);

        public static void EmptyRecycleBin()
        {
            const uint SHERB_NOCONFIRMATION = 0x00000001;
            const uint SHERB_NOPROGRESSUI = 0x00000002;
            const uint SHERB_NOSOUND = 0x00000004;

            SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
        }

        public static void CleanTemp()
        {
            // Implementierung zum Bereinigen des Temp-Ordners
        }

        public static void CleanPrefetch()
        {
            // Implementierung zum Bereinigen des Prefetch-Ordners
        }

        public static void CleanWindowsUpdateCache()
        {
            // Implementierung zum Bereinigen des Windows Update-Cache
        }

        public static void RunDiskCleanup()
        {
            // Implementierung zum Ausführen der Datenträgerbereinigung
        }

        public static void CleanRegistryEmptyKeys()
        {
            // Implementierung zum Bereinigen leerer Registrierungsschlüssel
        }
    }
}