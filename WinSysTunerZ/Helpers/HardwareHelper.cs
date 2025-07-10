using System.Collections.Generic;
using System.Management;
namespace WinSysTunerZ.Helpers {
    public class HardwareInfo { public string Name { get; set; } = ""; public string Value { get; set; } = ""; }
    public static class HardwareHelper {
        public static List<HardwareInfo> GetCpuInfo() {
            var list = new List<HardwareInfo>();
            var searcher = new ManagementObjectSearcher("SELECT Name,NumberOfCores,MaxClockSpeed FROM Win32_Processor");
            foreach(var mo in searcher.Get()){
                list.Add(new HardwareInfo { Name = "CPU Name", Value = mo["Name"]?.ToString() ?? "Unknown" });
                list.Add(new HardwareInfo { Name = "Cores", Value = mo["NumberOfCores"]?.ToString() ?? "Unknown" });
                list.Add(new HardwareInfo { Name = "Max Clock Speed", Value = mo["MaxClockSpeed"].ToString() + " MHz" });
            }
            return list;
        }
        // Analog: GetMemoryInfo(), GetStorageInfo(), GetGPUInfo() erweiterbar
    }
}