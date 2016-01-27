using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryDemo.Test.DesignPattern.Singleton
{
    [TestClass]
    public class SingletonTest
    {
        [TestMethod]
        public void TestCreateSingleton()
        {
            Singleton s1=Singleton.Instance;
            Singleton s2 =Singleton.Instance;
            
            Assert.AreEqual(s1,s2);
        }

        [TestMethod]
        public void TestNoPublicConstructors()
        {
            Type s = typeof (Singleton);
            ConstructorInfo[] ctrs = s.GetConstructors();
            bool hasPublicConstructor = false;
            foreach (var constructorInfo in ctrs)
            {
                if (constructorInfo.IsPublic)
                {
                    hasPublicConstructor = true;
                    break;
                }
            }

            Assert.IsFalse(hasPublicConstructor);
        }
    }
}
