using System;

namespace SalaryDemo.V2
{
    public class CommissionedTransaction : ChangeClassificationTransaction
    {
        public CommissionedTransaction(int empid) : base(empid)
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