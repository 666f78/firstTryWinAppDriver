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
        [Description("TC - 666")]
        [TestCaseSource(typeof(TestData), nameof(TestData.SingInWrongData))]
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

        [Test]
        public void DEBUG()
        {
            int id = 1;
            switch (id)
            {
                case 0:
                    Assert.Ignore(); //Ingore
                    break;
                case 1:
                    Assert.True((2 + 2) == 5, "Test Error Message"); //Fail
                    break;
                case 2:
                    Assert.True((2 + 2) == 4); //Pass
                    break;
                default:
                    break;
            }
        }
    }
}
