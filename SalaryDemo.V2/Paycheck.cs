using System;

namespace SalaryDemo.V2
{
    public class Paycheck
    {
        public Paycheck(DateTime startDate, DateTime payDate)
        {
            PayDate = payDate;
        }

        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }

        /// <summary>
        /// 扣减项
        /// </summary>
        public double Deductions { get; set; }
        public double NetPay { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public DateTime PayPeriodStartDate { get; set; }

        public string GetField(string disposition)
        {
            throw new NotImplementedException();
        }
    }
}