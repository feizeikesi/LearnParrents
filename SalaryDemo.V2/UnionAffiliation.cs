using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V2
{
    /// <summary>
    /// 工会
    /// </summary>
    public class UnionAffiliation : IAffiliation
    {

        private static List<ServiceCharge> list=new List<ServiceCharge>();
        public double Dues { get; set; }
        public int MemberId { get; set; }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return list.FirstOrDefault(a => a.Date == date);
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            list.Add(serviceCharge);
        }
    }
}