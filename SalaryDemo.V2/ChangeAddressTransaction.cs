namespace SalaryDemo.V2
{
    public class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        private readonly string _addrees;
        public ChangeAddressTransaction(int empid,string addrees) : base(empid)
        {
            _addrees = addrees;
        }

        protected override void Change(Employee employee)
        {
            employee.Address = _addrees;
        }
    }
}