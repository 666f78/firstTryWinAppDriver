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

        [Test]
        public void SingInWithWrongData()
        {
            new MainPage(driver)
                .OpenRemoteManager()
                .SendTextToEmail("123123")
                .SendTextToPassword("ewqqwe")
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
