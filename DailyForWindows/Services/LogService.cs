using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using DailyForWindows.Models;

namespace DailyForWindows.Services
{
    public class LogService
    {
        private readonly ReaderWriterLockSlim _lock = new();
        private readonly string _filePath;

        public LogService(string filePath)
        {
            _filePath = filePath;
        }

        public void Append(LogEntry entry)
        {
            var line = string.Format(CultureInfo.InvariantCulture, "{0},{1}", entry.Timestamp.ToString("o"), entry.Text.Replace("\n", " "));
            _lock.EnterWriteLock();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.AppendAllText(_filePath, line + Environment.NewLine, Encoding.UTF8);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IList<LogEntry> ReadAll()
        {
            _lock.EnterReadLock();
            try
            {
                if (!File.Exists(_filePath)) return new List<LogEntry>();
                return File.ReadAllLines(_filePath, Encoding.UTF8)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(ParseLine)
                    .ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void WriteAll(IEnumerable<LogEntry> entries)
        {
            _lock.EnterWriteLock();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.WriteAllLines(_filePath,
                    entries.Select(e => string.Format(CultureInfo.InvariantCulture, "{0},{1}", e.Timestamp.ToString("o"), e.Text.Replace("\n", " "))),
                    Encoding.UTF8);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private static LogEntry ParseLine(string line)
        {
            var comma = line.IndexOf(',');
            if (comma < 0) return new LogEntry { Timestamp = DateTime.Now, Text = line };
            var ts = DateTime.Parse(line.Substring(0, comma), null, DateTimeStyles.RoundtripKind);
            var text = line[(comma + 1)..];
            return new LogEntry { Timestamp = ts, Text = text };
        }
    }
}
