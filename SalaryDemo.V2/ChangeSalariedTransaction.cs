using System;

namespace SalaryDemo.V2
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public ChangeSalariedTransaction(int empid) : base(empid)
        {
        }

        public override IPaymentClassification Classification
        {
            get { throw new NotImplementedException(); }
        }

        public override IPaymentSchedule Schedule
        {
            get { throw new NotImplementedException(); }
        }
    }
}