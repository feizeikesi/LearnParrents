using System;

namespace SalaryDemo.V2
{
    public class WeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}