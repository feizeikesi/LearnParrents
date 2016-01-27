using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V2
{
    public class HourlyClassification : PaymentClassification
    {
        private static IList<TimeCard> list = new List<TimeCard>(); 

        private double _hourlyRate;
        public HourlyClassification(double hourlyRate)
        {
            _hourlyRate = hourlyRate;
        }
        public double Salary {
            get { return _hourlyRate; }
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return list.FirstOrDefault(a => a.Date == date);
        }

        public void AddTimeCard(TimeCard tc)
        {
            list.Add(tc);
        }
    }
}