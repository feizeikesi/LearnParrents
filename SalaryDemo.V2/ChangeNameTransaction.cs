namespace SalaryDemo.V2
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        private readonly string _name;
        public ChangeNameTransaction(int empId, string name) : base(empId)
        {
            _name = name;
        }

        protected override void Change(Employee employee)
        {
            employee.Name = _name;
        }
    }
}