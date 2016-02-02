using System;

namespace SalaryDemo.V2
{
    public class MonthlySchedule : IPaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return m2 == m1;
        }

        public bool IsPayDate(DateTime payDate)
        {
            return IsLastDayOfMonth(payDate);
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}