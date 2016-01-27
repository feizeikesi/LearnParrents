using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDemo.Test.DesignPattern.TemplateMethod
{
    /// <summary>
    /// 读取华氏温度转换成摄氏温度
    /// </summary>
   public  class FtoCRaw
    {
       public  void Run(string[] arges)
       {
           bool done = false;
           int i = 0;
           while (!done)
           {
               string fahrString = arges.ElementAtOrDefault(i);
               if (string.IsNullOrEmpty(fahrString))
               {
                   done = true;
               }
               else
               {
                   double fahr = Double.Parse(fahrString);
                   double celcius = 5.0 / 9.0 * (fahr - 32);
                   Console.Out.WriteLine("F={0},C={1}", fahr, celcius);
               }
               i++;
           }

           Console.Out.WriteLine("ftoc exit");
       }
    }
}
