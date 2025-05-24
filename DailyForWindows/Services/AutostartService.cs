using Microsoft.Win32;
using System;
using System.Reflection;

namespace DailyForWindows.Services
{
    public static class AutostartService
    {
        private const string RUN_KEY = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void EnableAutostart()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
            key?.SetValue("DailyForWindows", Assembly.GetExecutingAssembly().Location);
        }
    }
}
