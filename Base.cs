using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace task1
{
    [TestFixture]
    public class Base
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected WindowsDriver<WindowsElement> driver;
        private IConfiguration Config;

        [OneTimeSetUp]
        public void DoBeforeAllTests()
        {
            SetUpConfig();
            SetUpLog4net();
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
                TakeScreenshot();
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

        private void SetUpConfig()
        {
            Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private void TakeScreenshot()
        {
            driver.GetScreenshot();
            SaveScreenshot();

        }
        private void SaveScreenshot()
        {
            var fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var screenshotsDirectoryPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{Config.GetSection("Folders")["Screenshot"]}";
            if (!Directory.Exists(screenshotsDirectoryPath))
            {
                Directory.CreateDirectory(screenshotsDirectoryPath);
            }
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(@$"{screenshotsDirectoryPath}\{Guid.NewGuid()}_{fileDateTime}.jpg");
        }
        private void SetUpLog4net()
        {
            log4net.GlobalContext.Properties["LogFileName"] = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{Config.GetSection("Folders")["Logs"]}\logs";
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}