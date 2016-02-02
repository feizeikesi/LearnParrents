using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V2
{
    public class HourlyClassification : IPaymentClassification
    {
        private static readonly IList<TimeCard> TimeCards = new List<TimeCard>();

        private double _hourlyRate;
        public HourlyClassification(double hourlyRate)
        {
            _hourlyRate = hourlyRate;
        }
        public double Salary
        {
            get { return _hourlyRate; }
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return TimeCards.FirstOrDefault(a => a.Date == date);
        }

        public void AddTimeCard(TimeCard tc)
        {
            TimeCards.Add(tc);
        }

        public double CalculatePay(Paycheck paycheck)
        {
            return TimeCards
                .Where(timecard => IsInPayPeriod(timecard, paycheck))
                .Sum(timecard => CalculatePayForTimeCard(timecard));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timecard"></param>
        /// <returns></returns>
        private double CalculatePayForTimeCard(TimeCard timecard)
        {
            double overtimeHours = Math.Max(0.0, timecard.Hours - 8);
            double normalHours = timecard.Hours - overtimeHours;
            return _hourlyRate * normalHours + _hourlyRate * 1.5 * overtimeHours;
        }

        private bool IsInPayPeriod(TimeCard timecard, Paycheck paycheck)
        {
            DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
            DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;
            return timecard.Date >= payPeriodStartDate && timecard.Date <= payPeriodEndDate;
        }
    }
}