using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace task1
{
    [TestFixture]
    public class Base
    {
        protected WindowsDriver<WindowsElement> driver;

        [OneTimeSetUp]
        public void DoBeforeAllTests()
        {
            PowerShell.Create().AddCommand("Start-Process")
                   .AddParameter("FilePath", @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe")
                   .Invoke();
        }

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"D:\Program\TV\TeamViewer.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TestCleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
            Array.ForEach(Process.GetProcessesByName("TeamViewer"), x => x.Kill());
        }

        [OneTimeTearDown]
        public void OneTime() {
            Array.ForEach(Process.GetProcessesByName("WinAppDriver"), x => x.Kill());
        }
    }
}