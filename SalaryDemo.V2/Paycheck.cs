using System;

namespace SalaryDemo.V2
{
    public class Paycheck
    {
        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }

        public string GetField(string disposition)
        {
            throw new NotImplementedException();
        }
    }
}