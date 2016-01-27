

namespace SalaryDemo.Test.DesignPattern.Singleton
{
   public   class Singleton
   {
       /*
        * 1.好处
        *   1.跨平台
        *   2.适用于任何类
        *   3.可以透过派生创建
        *   4.延迟求值
        * 2.代价
        *   1.析构方法未定义
        *   2.不能继承
        *   3.效率问题
        *   4.不透明性
        */
       private static Singleton theInstance = null;
       private Singleton()
       {
       }

       public static Singleton Instance
       {
           get
           {
               if (theInstance==null)
               {
                   theInstance=new Singleton();
               }
               return theInstance;
           }
       }
    }
}
