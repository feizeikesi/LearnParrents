namespace SalaryDemo.V2
{
    public abstract class AddEmployeeTransaction : ITransaction
    {

        private readonly int _empid;
        private readonly string _name;
        private readonly string _address;
        public string Address { get; set; }


        protected AddEmployeeTransaction(int empid, string name, string address)
        {
            _empid = empid;
            _name = name;
            _address = address;
        }

        protected abstract IPaymentClassification MakeClassification();
        protected abstract IPaymentSchedule MakeSchedule();


        public int Execute()
        {
            IPaymentClassification pc = MakeClassification();
            IPaymentSchedule ps = MakeSchedule();
            IPayemntMethod  pm=new HoldMethod();

            Employee employee=new Employee();
            employee.Id = _empid;
            employee.Name = _name;
            employee.Address = _address;
            employee.Classification = pc;
            employee.Schedule = ps;
            employee.Method = pm;
            PayrollDatabase.AddEmployee(_empid, employee);
            return 1;
        }

    }
}