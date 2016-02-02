using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V2
{
    /// <summary>
    /// 工会
    /// </summary>
    public class UnionAffiliation : IAffiliation
    {

        private static List<ServiceCharge> list=new List<ServiceCharge>();
        public double Dues { get; set; }
        public int MemberId { get; set; }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return list.FirstOrDefault(a => a.Date == date);
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            list.Add(serviceCharge);
        }

        public double CalculateDeductions(Paycheck paycheck)
        {
            double totalDues = 0;
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);

            totalDues = Dues*fridays;
            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate)
        {
            int fridays = 0;

      
            for (DateTime day = payPeriodStartDate; day <= payPeriodEndDate;day.AddDays(1))
            {
                if (day.DayOfWeek==DayOfWeek.Friday)
                {
                    fridays++;
                }
            }

            return fridays;
        }
    }
}