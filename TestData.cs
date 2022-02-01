using NUnit.Framework;
using System.Collections.Generic;

namespace task1
{
    public static class TestData
    {
        public static IEnumerable<TestCaseData> SingInWrongData
        {
            get
            {
                yield return new TestCaseData("test 1", "321321");
                yield return new TestCaseData("test 2", "123123");
            }
        }
    }
}
