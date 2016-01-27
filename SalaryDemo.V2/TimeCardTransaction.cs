using System;

namespace SalaryDemo.V2
{
    public class TimeCardTransaction : ITransaction
    {

        private DateTime _date;
        private int _hours;
        private int _empid;
        public TimeCardTransaction(DateTime date, int hours, int empid)
        {
            _date = date;
            _hours = hours;
            _empid = empid;
        }

        public int Execute()
        {
            Employee e=  PayrollDatabase.GetEmployee(_empid);
            if (e != null)
            {
                HourlyClassification hc = e.Classification as HourlyClassification;
                if (hc != null)
                {
                    TimeCard tc = new TimeCard() {Date = _date, Hours = _hours};
                    hc.AddTimeCard(tc);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new InvalidOperationException("No such employee");
            }
      

            return 1;
        }
    }
}