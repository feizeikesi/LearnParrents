using System;
using System.Linq;

namespace SalaryDemo.Test.DesignPattern.Strategy
{
    public class FtoCStrategy:IApplication
    {
        private string[] fahrStrings = null;
        private string fahrString = string.Empty;
        private int i = 0;
        public void Init()
        {
            i = 0;
        }

        public void Idle()
        {
            fahrString = fahrStrings.ElementAtOrDefault(i);
            if (string.IsNullOrEmpty(fahrString))
            {
                return;
            }
            else
            {
                double fahr = Double.Parse(fahrString);
                double celcius = 5.0 / 9.0 * (fahr - 32);
                Console.Out.WriteLine("F={0},C={1}", fahr, celcius);
            }
            i++;
        }

        public void Cleanup()
        {
            Console.Out.WriteLine("ftoc exit");
        }

        public bool Done()
        {
            return fahrString.Length >= i;
        }
    }
}