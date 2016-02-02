using System;

namespace SalaryDemo.V2
{
    public class BiweeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            throw new NotImplementedException();
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}