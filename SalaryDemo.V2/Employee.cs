namespace SalaryDemo.V2
{
    /// <summary>
    /// 员工类
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public PaymentClassification Classification { get; set; }
        public IPaymentSchedule Schedule { get; set; }
        public PayemntMethod Method { get; set; }
        public IAffiliation Affiliation { get; set; }

    }
}