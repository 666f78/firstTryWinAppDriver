using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;

namespace task1.PageObjects
{
    public class MainPage
    {
        private WindowsDriver<WindowsElement> driver;


        private readonly By fileTransferLocator = By.XPath("//Button[@Name=\"7\"]");
        private readonly By fileTransferEmailLocator = By.XPath("//Edit[@Name=\"E-mail\"]");
        private readonly By fileTransferPasswordLocator = By.XPath("//Edit[@Name=\"Пароль\"]");
        private readonly By fileTransferSingInLocator = By.XPath("//Button[@Name=\"Войти в систему\"]");
        private readonly By fileTransferPopUpTextLocator = By.XPath("//Window[@Name =\"Компьютеры и контакты - ошибка\"]//Text");
        private readonly By fileTransferPopUpOkButtonLocator = By.XPath("//Window[@Name =\"Компьютеры и контакты - ошибка\"]//Button[@Name=\"OK\"]");
        private readonly By comboBoxLocator = By.XPath("//Pane[@AutomationId=\"dropdown-selector-id\"]");
        private readonly By comboBoxFileTransferLocator = By.XPath("//Pane[@ClassName=\"H-SMILE-POPUP-TRANSPARENT\"]/ListItem[@AutomationId=\"connection-option-2-id\"]/Text[@Name=\"Передача файлов\"]");
        private readonly By CheckStartWithWindowsLocator = By.XPath("//CheckBox[@AutomationId=\"start-with-system\"][@Name=\"Запускать TeamViewer при загрузке Windows\"]");



        public MainPage(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public MainPage OpenRemoteManager()
        {
            driver.FindElement(fileTransferLocator).Click();
            return this;
        }

        public MainPage SendTextToEmail(string testData)
        {
            driver.FindElement(fileTransferEmailLocator).Clear();
            driver.FindElement(fileTransferEmailLocator).SendKeys(testData);
            return this;
        }

        public MainPage SendTextToPassword(string testData)
        {
            driver.FindElement(fileTransferPasswordLocator).Clear();
            driver.FindElement(fileTransferPasswordLocator).SendKeys(testData);
            return this;
        }

        public MainPage ClickToSingIn()
        {
            driver.FindElement(fileTransferSingInLocator).Click();
            return this;
        }
        public MainPage CheckMessageBoxWrongData()
        {
            Thread.Sleep(2000);
            var popUpText = driver.FindElement(fileTransferPopUpTextLocator).Text;
            Assert.IsTrue(popUpText.Contains("Имя пользователя и пароль, которые вы ввели, не совпадают ни с какими учетными записями"));
            driver.FindElement(fileTransferPopUpOkButtonLocator).Click();
            return this;
        }
        public MainPage SelectFileTransfer()
        {
            driver.FindElement(comboBoxLocator).Click();
            driver.FindElement(comboBoxFileTransferLocator).Click();
            return this;
        }

        public MainPage CheckStartWithWindows()
        {
            driver.FindElement(CheckStartWithWindowsLocator).Click();
            return this;
        }
    }
}
