using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.Strategy
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestBubbleSorter()
        {
            var data = new[] { 3, 5, 2, 10, 4, 9, 7, 6, 8, 1};
            BubbleSorter.Sort(data);
            Assert.AreEqual("1,2,3,4,5,6,7,8,9,10", string.Join(",", data));
        }
    }
}