using NUnit.Framework;
using task1.PageObjects;

namespace task1
{
    [TestFixture]
    internal class Tests : Base
    {
        [Test]
        public void ClickToTheStartWithSystemButton()
        {
            new MainPage(driver)
                .CheckStartWithWindows();
        }

        [Test, TestCaseSource(nameof(TestData.SingInWrongData))]
        public void SingInWithWrongData(string Email, string Password)
        {
            new MainPage(driver)
                .OpenRemoteManager()
                .SendTextToEmail(Email)
                .SendTextToPassword(Password)
                .ClickToSingIn()
                .CheckMessageBoxWrongData();
        }

        [Test]
        public void ChangeTheSelectBox()
        {
            new MainPage(driver)
                .SelectFileTransfer();
        }
    }
}
