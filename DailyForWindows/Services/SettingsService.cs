using System;
using System.IO;
using System.Text.Json;
using DailyForWindows.Models;

namespace DailyForWindows.Services
{
    public class SettingsService
    {
        private readonly string _path;

        public SettingsService(string path)
        {
            _path = path;
        }

        public AppSettings Load()
        {
            try
            {
                if (File.Exists(_path))
                {
                    var json = File.ReadAllText(_path);
                    var settings = JsonSerializer.Deserialize<AppSettings>(json);
                    if (settings != null) return settings;
                }
            }
            catch { }
            return new AppSettings { LogFilePath = GetDefaultLogPath() };
        }

        public void Save(AppSettings settings)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_path, json);
            }
            catch { }
        }

        public static string GetDefaultSettingsPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "DailyForWindows", "settings.json");
        }

        public static string GetDefaultLogPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "DailyForWindows", "log.csv");
        }
    }
}
