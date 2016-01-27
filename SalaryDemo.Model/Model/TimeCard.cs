using System;

namespace SalaryDemo.V1.Model
{
    /// <summary>
    /// 考勤卡
    /// </summary>
    public class TimeCard : Entity<int>
    {
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}