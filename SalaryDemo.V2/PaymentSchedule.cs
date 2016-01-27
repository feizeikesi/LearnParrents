using System;

namespace SalaryDemo.V2
{
    public interface IPaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
    }
}