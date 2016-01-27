using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.Monostate
{
    [TestClass]
    public class TurnstileTest
    {
        /*
         * 旋转门(Turnstile)开始时处于Locked状态，如果投入一枚硬币（Coin），它就迁移到UnLocked状态，
         * 开启旋转门，重置可能出现的任何警告（Alarm）状态，并把硬币放到投币箱（Deposit）中
         * 如果此时乘客通过（Pass）了旋转门，旋转门就迁移回Locked状态并且把门锁上
         * 如果乘客在通过旋转门投入过多的硬币，则多余的硬币被退还（Refund），并且旋转门保持在Unlocked状态
         * 如果乘客没有投币就想通过旋转门，那么会发出警报，并且旋转门保持Locked状态
         */


        [TestInitialize]
        public void Init()
        {
            Turnstile t=new Turnstile();
            t.Reset();
           
        }

        /// <summary>
        /// 旋转门开始时处于Locked状态，没有警告
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            Turnstile t=new Turnstile();
            Assert.IsTrue(t.Locked());
            Assert.IsFalse(t.Alarm());
        }

        /// <summary>
        /// 多个旋转门对象公用状态
        /// </summary>
          [TestMethod]
        public void TestCoin()
        {
              Turnstile t=new Turnstile();
              t.Coin();
              Turnstile t1=new Turnstile();
              Assert.IsFalse(t1.Locked());
              Assert.IsFalse(t1.Alarm());
              Assert.AreEqual(1,t.Coins);
        }

        /// <summary>
        /// 用户投币经过旋转门
        /// </summary>
          [TestMethod]
          public void TestCoinAndPass()
          {
              Turnstile t=new Turnstile();
              t.Coin();
              t.Pass();
              Turnstile t1=new Turnstile();

             Assert.IsTrue(t1.Locked());
              Assert.IsFalse(t1.Alarm());
              Assert.AreEqual(1,t1.Coins,"coins");
          }

          [TestMethod]
          public void TestTwoCoins()
          {
              Turnstile t=new Turnstile();
              t.Coin();
              t.Coin();
              Turnstile t1=new Turnstile();
              Assert.IsFalse(t1.Locked());
              Assert.AreEqual(1,t1.Coins);
              Assert.AreEqual(1,t1.Refunds);
              Assert.IsFalse(t1.Alarm());
          }

          [TestMethod]
          public void TestPass()
          {
              Turnstile t=new Turnstile();
              t.Pass();
              Turnstile t1 =new Turnstile();
              Assert.IsTrue(t1.Alarm());
              Assert.IsTrue(t1.Locked());
          }

          [TestMethod]
          public void TestCancelAlarm()
          {
              Turnstile t=new Turnstile();
              t.Pass();
              t.Coin();
              Turnstile t1 = new Turnstile();
              Assert.IsFalse(t1.Alarm());
              Assert.IsFalse(t1.Locked());
              Assert.AreEqual(1,t1.Coins);
              Assert.AreEqual(0,t1.Refunds);
          }

          [TestMethod]
          public void TestTwoOperations()
          {
              Turnstile t=new Turnstile();
              t.Coin();
              t.Pass();
              t.Coin();
              Turnstile t1=new Turnstile();
              Assert.IsFalse(t1.Locked());
              Assert.AreEqual(2,t1.Coins);
              t.Pass();
              Assert.IsTrue(t1.Locked());
          }
    }
}
