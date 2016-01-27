using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.ActiveObject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSleep()
        {
            WakeUpCommand wakeUp=new WakeUpCommand();
            ActionObjectEngine action=new ActionObjectEngine();
            SleepCommand sleep=new SleepCommand(1000,action,wakeUp);
            action.AddCommand(sleep);
            DateTime start=DateTime.Now;
            action.Run();
            DateTime stop=DateTime.Now;

            double sleepTime = (stop - start).TotalMilliseconds;

            Assert.IsTrue(sleepTime>=1000,"SleetTime "+sleepTime+" expected>1000");
            //Assert.IsTrue(sleepTime <= 1000, "SleetTime " + sleepTime + " expected<1000");
            Assert.IsTrue(wakeUp.Executed,"Commend Executed");
        }
    }
}
