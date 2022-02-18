using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;

namespace task1
{
    public static class Extensions
    {
        public static bool CheckPopUpMessageWithWrongData(this string str)
        {
            return str.Contains("Имя пользователя и пароль, которые вы ввели, не совпадают ни с какими учетными записями");
        }

        public static void FindAndClick(this WindowsDriver<WindowsElement> driver, By locator)
        {
            driver.FindElement(locator).Click();
        }

        public static void FindAndClear(this WindowsDriver<WindowsElement> driver, By locator)
        {
            driver.FindElement(locator).Clear();
        }

        public static void TakeScreenshot(WindowsDriver<WindowsElement> driver, string path)
        {
            driver.GetScreenshot();
            SaveScreenshot(driver, path);

        }
        private static void SaveScreenshot(WindowsDriver<WindowsElement> driver, string path)
        {
            var fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var screenshotsDirectoryPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{path}";
            if (!Directory.Exists(screenshotsDirectoryPath))
            {
                Directory.CreateDirectory(screenshotsDirectoryPath);
            }
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(@$"{screenshotsDirectoryPath}\{Guid.NewGuid()}_{fileDateTime}.jpg");
        }
    }
}
