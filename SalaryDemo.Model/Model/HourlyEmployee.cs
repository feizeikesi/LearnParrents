using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V1.Model
{
    public class HourlyEmployee : Employee
    {
        public IList<TimeCard> TimeCards { get; set; }

        public override double Calculate()
        {
            var hour = 0;
            if (hour > 8)
            {
                return 8*HourPay + (hour - 8)*HourPay*CallbackPay;
            }
            else
            {
                return hour*HourPay;
            }
        }

        public double HourPay { get; set; }

        public double CallbackPay { get; set; }

        public ICollection<TimeCard> GeTimeCards()
        {
            //获得一个周的打卡记录
            return TimeCards;
        }
    }


}