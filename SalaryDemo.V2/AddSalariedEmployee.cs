namespace SalaryDemo.V2
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double _salary;
        public AddSalariedEmployee(int empid, string name, string address, double salary)
            : base(empid, name, address)
        {
            _salary = salary;
        }


        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(_salary);
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}