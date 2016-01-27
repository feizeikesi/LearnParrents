namespace SalaryDemo.V2
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double _hourlyRate;
        public AddHourlyEmployee(int empid, string name, string address,double hourlyRate) : base(empid, name, address)
        {
            _hourlyRate = hourlyRate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(_hourlyRate);
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new WeeklySchedule();
        }
    }
}