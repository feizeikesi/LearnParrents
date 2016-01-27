using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDemo.Test.DesignPattern.TemplateMethod
{
   public  abstract class Application
   {
       private bool isDone = false;

       /// <summary>
       /// 初始化
       /// </summary>
       protected abstract void Init();

       /// <summary>
       /// 
       /// </summary>
       protected abstract void Idle();

       /// <summary>
       /// 处理程序退出前所做的清理工作
       /// </summary>
       protected abstract void Cleanup();

       protected void SetDone()
       {
           isDone = true;
       }

       protected bool Done()
       {
           return isDone;
       }

       public void Run()
       {
           Init();
           while (!Done())
           {
               Idle();
           }
           Cleanup();
       }
   }
}
