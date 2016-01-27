using System;

namespace SalaryDemo.V1.Model
{
    /// <summary>
    /// 服务费
    /// </summary>
    public class ServiceCharge : Entity<int>
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }

}
