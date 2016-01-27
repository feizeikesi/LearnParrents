using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.TemplateMethod
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestFtoCRaw()
        {
            var ftoC=new FtoCRaw();
            ftoC.Run(new[] {"1"});
        }


        [TestMethod]
        public void TestFtoCTemplateMethod()
        {
            var ftoC = new FtoCTemplateMethod();
            ftoC.Run(new[] { "1" });
        }

        [TestMethod]
        public void TestBubbleSort()
        {
            var data = new int[] {1, 3, 5, 2, 10, 4, 9, 7,6,8};
            BubbleSorter.Sort(data);
            Assert.AreEqual("1,2,3,4,5,6,7,8,9,10", string.Join(",", data));
        }

        [TestMethod]
        public void TestIntBubbleSort()
        {
            var data = new int[] {1, 3, 5, 2, 10, 4, 9, 7,6,8};
            new IntBubbleSorter().Sort(data);
            Assert.AreEqual("1,2,3,4,5,6,7,8,9,10", string.Join(",", data));
        }


    }
}
