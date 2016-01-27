
namespace SalaryDemo.V1.Model
{
    public interface IPayclassification
    {
        double Calculate();
    }

    public abstract class Employee : Entity<int>, IPayclassification
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public  string PaymentMode { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 工会
        /// </summary>
        public string LaborUnion { get; set; }

        public abstract double Calculate();
    }
}