using System;
using System.Collections.Generic;
using Microsoft.Win32;
using WinSysTunerZ.Helpers;

namespace WinSysTunerZ.Models
{
    public class RegistrySetting
    {
        public string Name { get; set; }
        public string Path { get; set; }              // <-- Path, nicht RegistryPath!
        public string Key { get; set; }
        public object DefaultValue { get; set; }
        public object CurrentValue { get; private set; }
        public string Description { get; set; }

        public RegistrySetting(string name, string path, string key, object defaultValue, string description)
        {
            Name = name;
            Path = path;      // <-- Path!
            Key = key;
            DefaultValue = defaultValue;
            Description = description;

            CurrentValue = DefaultValue;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                RefreshCurrent();
        }

        public static List<RegistrySetting> GetDefaults() => new()
        {
            new RegistrySetting(
                name: "Enable Foo",
                path: @"Software\MyApp\Foo",
                key: "Enabled",
                defaultValue: 1,
                description: "Schaltet Foo ein/aus"
            ),
            // ... weitere Settings
        };

        public void RefreshCurrent()
        {
            CurrentValue = RegistryHelper.GetValue(
                RegistryHive.CurrentUser,
                Path,   // <-- Path!
                Key,
                DefaultValue
            );
        }

        public void ApplyDefault()
        {
            RegistryHelper.SetValue(
                RegistryHive.CurrentUser,
                Path,  // <-- Path!
                Key,
                DefaultValue
            );
            RefreshCurrent();
        }

        public void ResetToDefault() => ApplyDefault();
    }
}
