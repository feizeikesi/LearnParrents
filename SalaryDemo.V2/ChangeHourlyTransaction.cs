namespace SalaryDemo.V2
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        private readonly double _hourlyRate;
        public ChangeHourlyTransaction(int empid,double hourlyRate) : base(empid)
        {
            _hourlyRate = hourlyRate;
        }

        public override PaymentClassification Classification
        {
            get { return new HourlyClassification(_hourlyRate);}
        }

        public override IPaymentSchedule Schedule
        {
            get { return  new WeeklySchedule(); }
        }
    }
}