using System;

namespace SalaryDemo.V2
{
    /// <summary>
    /// 员工类
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 支付分类
        /// </summary>
        public IPaymentClassification Classification { get; set; }

        /// <summary>
        /// 支付计划
        /// </summary>
        public IPaymentSchedule Schedule { get; set; }

        /// <summary>
        /// 支付方法
        /// </summary>
        public IPayemntMethod Method { get; set; }

        /// <summary>
        /// 工会
        /// </summary>
        public IAffiliation Affiliation { get; set; }

        public void Payday(Paycheck paycheck)
        {
            double grossPay = Classification.CalculatePay(paycheck);
            double deductions = Affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            Method.Pay(paycheck);
        }

        public bool IsPayDate(DateTime payDate)
        {
            return Schedule.IsPayDate(payDate);
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            return Schedule.GetPayPeriodStartDate(date);
        }
    }
}