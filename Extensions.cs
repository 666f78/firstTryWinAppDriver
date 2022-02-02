using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    public static class Extensions
    {
        public static bool CheckPopUpMessageWithWrongData(this string str)
        {
            return str.Contains("Имя пользователя и пароль, которые вы ввели, не совпадают ни с какими учетными записями");
        }

        public static void FindAndClick(this WindowsDriver<WindowsElement> driver,By locator)
        {
            driver.FindElement(locator).Click();
        }

        public static void FindAndClear(this WindowsDriver<WindowsElement> driver, By locator)
        {
            driver.FindElement(locator).Clear();
        }
    }
}
