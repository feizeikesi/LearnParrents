using System;

namespace SalaryDemo.V2
{
    /// <summary>
    /// 支付计划接口
    /// </summary>
    public interface IPaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
        DateTime GetPayPeriodStartDate(DateTime date);
    }
}