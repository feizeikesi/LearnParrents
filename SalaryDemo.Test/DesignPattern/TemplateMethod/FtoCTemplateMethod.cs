using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDemo.Test.DesignPattern.TemplateMethod
{
  public   class FtoCTemplateMethod:Application
  {
      private string[] fahrStrings =null;
      private string fahrString = string.Empty;
      private   int i = 0;
      protected override void Init()
      {
          i = 0;
      }

      protected override void Idle()
      {

          fahrString = fahrStrings.ElementAtOrDefault(i);
          if (string.IsNullOrEmpty(fahrString))
          {
              SetDone();
          }
          else
          {
              double fahr = Double.Parse(fahrString);
              double celcius = 5.0 / 9.0 * (fahr - 32);
              Console.Out.WriteLine("F={0},C={1}", fahr, celcius);
          }
          i++;
      }

      protected override void Cleanup()
      {
          Console.Out.WriteLine("ftoc exit");
      }

      public void Run(string[] arges)
      {
          fahrStrings = arges;
          base.Run();
      }
  }
}
