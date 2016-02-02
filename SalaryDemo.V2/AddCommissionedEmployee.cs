namespace SalaryDemo.V2
{
    /// <summary>
    /// 添加一个销售人员
    /// </summary>
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double _salary;
        private readonly double _commissionRate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="salary"></param>
        /// <param name="commissionRate"></param>
        public AddCommissionedEmployee(int empid, string name, string address,double salary, double commissionRate)
            : base(empid, name, address)
        {
            _salary = salary;
            _commissionRate = commissionRate;
        }

        protected override IPaymentClassification MakeClassification()
        {
            return new CommissionedClassification();
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}