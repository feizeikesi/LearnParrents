using System;
using System.Collections.Generic;

namespace SalaryDemo.V2
{
    public class  PaydayTransaction:ITransaction
    {
        private Dictionary<int,Paycheck>  paychecks=new Dictionary<int, Paycheck>();
        private readonly DateTime _payDate;
        public PaydayTransaction(DateTime payDate)
        {
            _payDate = payDate;
        }


        public int Execute()
        {
            IEnumerable<int> ids= PayrollDatabase.GetAllEmployeeIds();
            foreach (var empId in ids)
            {
                Employee employee = PayrollDatabase.GetEmployee(empId);
                if (employee.IsPayDate(_payDate))
                {
                    DateTime startDate = employee.GetPayPeriodStartDate(_payDate);

                    Paycheck pc = new Paycheck(startDate, _payDate);
                    paychecks[empId] = pc;
                    employee.Payday(pc);
                }
            }
            return 0;
        }

        public Paycheck GetPaycheck(int empId)
        {
            throw new NotImplementedException();
        }
    }
}