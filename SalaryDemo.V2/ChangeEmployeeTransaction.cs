using System;

namespace SalaryDemo.V2
{
    public abstract class ChangeEmployeeTransaction:ITransaction
    {
        private readonly int _empId;
        public ChangeEmployeeTransaction(int empid)
        {
            this._empId = empid;
        }

        public int Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(_empId);
            if (e!=null)
            {
                Change(e);
                return 1;
            }
            else
            {
                throw new InvalidOperationException();
            }
            return 0;
        }

        protected abstract void Change(Employee employee);
    }
}