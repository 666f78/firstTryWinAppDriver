using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.Management.Automation;
using task1.Config;
using task1.Log;

namespace task1
{
    [TestFixture]
    public class Base
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected WindowsDriver<WindowsElement> driver;
        private readonly IConfiguration Config = new ConfigSetUp().Config;

        [OneTimeSetUp]
        public void DoBeforeAllTests()
        {
            new Logger(Config.GetSection("Folders")["Logs"]);
            StartWinDriver();
        }

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", Config.GetSection("Settings")["AppPath"]);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            log.Info($"Start test case {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TestCleanup()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Ignored)
            {
                string msg = TestContext.CurrentContext.Test.FullName + " - Ingore";
                log.Info(msg);
            }
            if ((TestContext.CurrentContext.Result.Outcome == ResultState.Failure) || (TestContext.CurrentContext.Result.Outcome == ResultState.Error))
            {
                string error = TestContext.CurrentContext.Test.FullName + TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                log.Error(error);
                Extensions.TakeScreenshot(driver, Config.GetSection("Folders")["Screenshot"]);
            }
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            {
                string msg = TestContext.CurrentContext.Test.FullName + " - Passed";
                log.Info(msg);
            }
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
            Array.ForEach(Process.GetProcessesByName("TeamViewer"), x => x.Kill());
        }

        [OneTimeTearDown]
        public void OneTime()
        {
            Array.ForEach(Process.GetProcessesByName("WinAppDriver"), x => x.Kill());
        }

        private void StartWinDriver()
        {
            PowerShell.Create().AddCommand("Start-Process")
               .AddParameter("FilePath", Config.GetSection("Settings")["WinDriverPath"])
               .Invoke();
        }
    }
}