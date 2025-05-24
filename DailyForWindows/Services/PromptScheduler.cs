using System;
using System.Threading;
using DailyForWindows.Models;

namespace DailyForWindows.Services
{
    public class PromptScheduler : IDisposable
    {
        private Timer? _timer;
        private readonly Action _callback;
        private AppSettings _settings;
        private DateTime _nextTime;

        public PromptScheduler(AppSettings settings, Action callback)
        {
            _settings = settings;
            _callback = callback;
        }

        public void UpdateSettings(AppSettings settings)
        {
            _settings = settings;
            ScheduleNext();
        }

        public void Start()
        {
            ScheduleNext();
        }

        private void ScheduleNext()
        {
            _timer?.Dispose();
            var now = DateTime.Now;
            var next = now + TimeSpan.FromMinutes(_settings.IntervalMinutes);
            if (next.TimeOfDay < _settings.StartHour)
                next = now.Date + _settings.StartHour;
            if (next.TimeOfDay > _settings.EndHour)
                next = now.Date.AddDays(1) + _settings.StartHour;
            _nextTime = next;
            var due = next - now;
            _timer = new Timer(_ => _callback(), null, due, Timeout.InfiniteTimeSpan);
        }

        public void Snooze(int minutes)
        {
            _timer?.Dispose();
            var now = DateTime.Now;
            _nextTime = now + TimeSpan.FromMinutes(minutes);
            _timer = new Timer(_ => _callback(), null, TimeSpan.FromMinutes(minutes), Timeout.InfiniteTimeSpan);
        }

        public DateTime NextTime => _nextTime;

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
