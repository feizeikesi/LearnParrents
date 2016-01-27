using System;

namespace SalaryDemo.V1.Model
{
    /// <summary>
    /// 销售凭条
    /// </summary>
    public class SalesReceipt : Entity<int>
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }

        public double Amount { get; set; }
    }

    class SalesReceiptImpl : SalesReceipt
    {
    }
}