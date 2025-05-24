using System;

namespace DailyForWindows.Models
{
    public class AppSettings
    {
        public int IntervalMinutes { get; set; } = 30;
        public int SnoozeMinutes { get; set; } = 5;
        public string LogFilePath { get; set; } = string.Empty;
        public TimeSpan StartHour { get; set; } = TimeSpan.FromHours(9);
        public TimeSpan EndHour { get; set; } = TimeSpan.FromHours(17);
    }
}
