using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.Monostate.V1
{
    [TestClass]
    public class TurnstileTest
    {

        [TestInitialize]
        public void Init()
        {
            Turnstile t = new Turnstile();
            t.Reset();

        }

        /// <summary>
        /// 旋转门开始时处于Locked状态，没有警告
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            Turnstile t = new Turnstile();
            Assert.IsTrue(t.Locked());
            Assert.IsFalse(t.Alarm());
        }

        /// <summary>
        /// 多个旋转门对象公用状态
        /// </summary>
        [TestMethod]
        public void TestCoin()
        {
            Turnstile t = new Turnstile();
            t.Coin();
            //Turnstile t1 = new Turnstile();
            Assert.IsFalse(t.Locked());
            Assert.IsFalse(t.Alarm());
            Assert.AreEqual(1, t.Coins);
        }

        /// <summary>
        /// 用户投币经过旋转门
        /// </summary>
        [TestMethod]
        public void TestCoinAndPass()
        {
            Turnstile t = new Turnstile();
            t.Coin();
            t.Pass();
            //Turnstile t1 = new Turnstile();

            Assert.IsTrue(t.Locked());
            Assert.IsFalse(t.Alarm());
            //Assert.AreEqual(1, t1.Coins, "coins");
        }

        [TestMethod]
        public void TestTwoCoins()
        {
            Turnstile t = new Turnstile();
            t.Coin();
            t.Coin();
            Assert.IsFalse(t.Locked());
            Assert.AreEqual(1, t.Coins);
            Assert.AreEqual(1, t.Refunds);
            Assert.IsFalse(t.Alarm());
        }

        [TestMethod]
        public void TestPass()
        {
            Turnstile t = new Turnstile();
            t.Pass();
            //Turnstile t1 = new Turnstile();
            Assert.IsTrue(t.Alarm());
            Assert.IsTrue(t.Locked());
        }

        [TestMethod]
        public void TestCancelAlarm()
        {
            Turnstile t = new Turnstile();
            t.Pass();
            t.Coin();
            
            Assert.IsFalse(t.Alarm());
            Assert.IsFalse(t.Locked());
            Assert.AreEqual(1, t.Coins);
            Assert.AreEqual(0, t.Refunds);
        }

        [TestMethod]
        public void TestTwoOperations()
        {
            Turnstile t = new Turnstile();
            t.Coin();
            t.Pass();
            t.Coin();
            //Turnstile t1 = new Turnstile();
            Assert.IsFalse(t.Locked());
            Assert.AreEqual(2, t.Coins);
            t.Pass();
            Assert.IsTrue(t.Locked());
        }
    }
}
