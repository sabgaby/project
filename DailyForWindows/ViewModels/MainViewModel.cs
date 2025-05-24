using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DailyForWindows.Models;
using DailyForWindows.Services;

namespace DailyForWindows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly LogService _logService;
        private readonly PromptScheduler _scheduler;
        private readonly SettingsService _settingsService;
        private AppSettings _settings;

        public ObservableCollection<LogEntry> Entries { get; } = new();

        public MainViewModel()
        {
            _settingsService = new SettingsService(SettingsService.GetDefaultSettingsPath());
            _settings = _settingsService.Load();
            if (string.IsNullOrEmpty(_settings.LogFilePath))
                _settings.LogFilePath = SettingsService.GetDefaultLogPath();
            _logService = new LogService(_settings.LogFilePath);
            foreach (var e in _logService.ReadAll().OrderByDescending(e => e.Timestamp))
                Entries.Add(e);
            _scheduler = new PromptScheduler(_settings, OnTimer);
            _scheduler.Start();
        }

        private void OnTimer()
        {
            Application.Current.Dispatcher.Invoke(() => Prompt());
        }

        public void Prompt()
        {
            var window = new Views.PromptWindow { DataContext = this };
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public void AddEntry(string text)
        {
            var entry = new LogEntry { Timestamp = DateTime.Now, Text = text };
            Entries.Insert(0, entry);
            try
            {
                _logService.Append(entry);
            }
            catch
            {
                MessageBox.Show("Failed to write log file.");
            }
            _scheduler.Start();
            OnPropertyChanged(nameof(NextPromptTime));
        }

        public void Snooze()
        {
            _scheduler.Snooze(_settings.SnoozeMinutes);
            OnPropertyChanged(nameof(NextPromptTime));
        }

        public DateTime NextPromptTime => _scheduler.NextTime;

        public void SaveSettings(AppSettings settings)
        {
            _settings = settings;
            _settingsService.Save(settings);
            _logService.WriteAll(Entries);
            _scheduler.UpdateSettings(settings);
            OnPropertyChanged(nameof(NextPromptTime));
        }

        public AppSettings Settings => _settings;
    }
}
