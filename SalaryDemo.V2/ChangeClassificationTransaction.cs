namespace SalaryDemo.V2
{
    public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        public ChangeClassificationTransaction(int empid) : base(empid)
        {
        }

        protected override void Change(Employee employee)
        {
            employee.Classification = Classification;
            employee.Schedule = Schedule;
        }

        public abstract IPaymentClassification Classification { get; }

        public abstract IPaymentSchedule Schedule { get;  }
    }
}