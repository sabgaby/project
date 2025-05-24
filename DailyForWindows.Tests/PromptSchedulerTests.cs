using System;
using DailyForWindows.Models;
using DailyForWindows.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyForWindows.Tests
{
    [TestClass]
    public class PromptSchedulerTests
    {
        [TestMethod]
        public void SnoozeChangesNextTime()
        {
            var settings = new AppSettings { IntervalMinutes = 30 };
            var scheduler = new PromptScheduler(settings, () => { });
            scheduler.Start();
            var before = scheduler.NextTime;
            scheduler.Snooze(10);
            Assert.IsTrue(scheduler.NextTime > before && scheduler.NextTime - DateTime.Now <= TimeSpan.FromMinutes(10));
            scheduler.Dispose();
        }
    }
}
