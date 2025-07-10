using System.Runtime.Versioning;
using Microsoft.Win32;

namespace WinSysTunerZ.Helpers {
    public static class RegistryHelper {
        [SupportedOSPlatform("windows")]
        public static void SetValue(RegistryHive hive, string path, string name, object value) {
            using var key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).CreateSubKey(path);
            key.SetValue(name, value);
        }

        [SupportedOSPlatform("windows")]
        public static object GetValue(RegistryHive hive, string path, string name, object defaultVal) {
            using var key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenSubKey(path);
            return key?.GetValue(name, defaultVal) ?? defaultVal;
        }

        [SupportedOSPlatform("windows")]
        public static void ResetValue(RegistryHive hive, string path, string name, object defaultVal) {
            SetValue(hive, path, name, defaultVal);
        }
    }
}