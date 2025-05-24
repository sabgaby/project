using System;

namespace DailyForWindows.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
