using NUnit.Framework;
using System.Collections.Generic;
using task1.PageObjects;

namespace task1
{
    [TestFixture]
    internal class Tests : Base
    {
        private static IEnumerable<TestCaseData> SingInWrongData
        {
            get
            {
                yield return new TestCaseData("test 1", "321321");
                yield return new TestCaseData("test 2", "123123");
            }
        }

        [Test]
        public void ClickToTheStartWithSystemButton()
        {
            new MainPage(driver)
                .CheckStartWithWindows();
        }

        [Test, TestCaseSource(nameof(SingInWrongData))]
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
