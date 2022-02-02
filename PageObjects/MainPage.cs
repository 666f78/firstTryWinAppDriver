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
            driver.FindAndClick(fileTransferLocator);
            return this;
        }

        public MainPage SendTextToEmail(string testData)
        {
            SendTextTo(testData,"Email");
            return this;
        }

        public MainPage SendTextToPassword(string testData)
        {
            SendTextTo(testData, "Password");
            return this;
        }

        public MainPage ClickToSingIn()
        {
            driver.FindAndClick(fileTransferSingInLocator);
            return this;
        }
        public MainPage CheckMessageBoxWrongData()
        {
            Thread.Sleep(2000);
            var popUpText = driver.FindElement(fileTransferPopUpTextLocator).Text;
            Assert.IsTrue(popUpText.CheckPopUpMessageWithWrongData());
            driver.FindAndClick(fileTransferPopUpOkButtonLocator);
            return this;
        }
        public MainPage SelectFileTransfer()
        {
            driver.FindAndClick(comboBoxLocator);
            driver.FindAndClick(comboBoxFileTransferLocator);
            return this;
        }

        public MainPage CheckStartWithWindows()
        {
            driver.FindAndClick(CheckStartWithWindowsLocator);
            return this;
        }
        private void SendTextTo(string testData, string to)
        {
            By locator = null;
            switch (to)
            {
                case "Email":
                    locator = fileTransferEmailLocator;
                    break;
                case "Password":
                    locator = fileTransferPasswordLocator;
                    break;
                default:
                    break;
            }
            driver.FindAndClear(locator);
            driver.FindElement(locator).SendKeys(testData);
        }
    }
}
