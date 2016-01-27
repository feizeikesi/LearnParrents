using System;

namespace SalaryDemo.V2
{
    public class  ServiceChargeTransaction:ITransaction
    {
        private int _memberId;
        private DateTime _date;
        private double _charge;

        public ServiceChargeTransaction(int memberId, DateTime date,double charge)
        {
            _memberId = memberId;
            _charge = charge;
            _date = date;
        }

        public int Execute()
        {
            Employee e = PayrollDatabase.GetUnionMember(_memberId);
            if (e!=null)
            {
                UnionAffiliation ua = null;
                if (e.Affiliation is UnionAffiliation)
                {
                    ua=e.Affiliation as UnionAffiliation;
                }

                if (ua!=null)
                {
                    ua.AddServiceCharge(new ServiceCharge(){Amount = _charge,Date = _date});
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw  new InvalidOperationException();
            }
            return 0;
        }
    }
}