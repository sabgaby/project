using System;
using System.IO;
using DailyForWindows.Models;
using DailyForWindows.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyForWindows.Tests
{
    [TestClass]
    public class LogServiceTests
    {
        [TestMethod]
        public void WriteAndReadEntries()
        {
            var tmp = Path.GetTempFileName();
            try
            {
                var log = new LogService(tmp);
                var e = new LogEntry { Timestamp = DateTime.Now, Text = "test" };
                log.Append(e);
                var list = log.ReadAll();
                Assert.AreEqual(1, list.Count);
                Assert.AreEqual("test", list[0].Text);
            }
            finally
            {
                File.Delete(tmp);
            }
        }
    }
}
