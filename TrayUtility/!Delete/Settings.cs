using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace UtilitiesHandler
{
    public class Settings
    {
        public string SettingsDirectoryPath { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Process.GetCurrentProcess().MainModule.FileName.Split('\\').Last().Split('.').First());
        public string FileName { get; set; } = "DisabledUtilities.json";
        List<SettingsItem> _settingsItems = new List<SettingsItem>();
        public Settings(List<Utility> disabledUtilityItems)
        {
            foreach (var item in disabledUtilityItems)
                _settingsItems.Add(new SettingsItem() { UtilityName = item.Name });

            var k = SettingsDirectoryPath;

            saveSettings();
            readSettings();
        }

        private void saveSettings()
        {
            if (!Directory.Exists(SettingsDirectoryPath))
                Directory.CreateDirectory(SettingsDirectoryPath);

            File.WriteAllText($"{SettingsDirectoryPath}\\{FileName}", JsonConvert.SerializeObject(_settingsItems));
        }

        private void readSettings()
        {
            if (!Directory.Exists(SettingsDirectoryPath))
                Directory.CreateDirectory(SettingsDirectoryPath);

            List<SettingsItem> settingsItems = JsonConvert.DeserializeObject<List<SettingsItem>>(File.ReadAllText($"{SettingsDirectoryPath}\\{FileName}"));
            _settingsItems = settingsItems;
        }
    }
}
