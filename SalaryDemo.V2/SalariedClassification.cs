namespace SalaryDemo.V2
{
    /// <summary>
    /// 月薪
    /// </summary>
    public class SalariedClassification : IPaymentClassification
    {
        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double Salary { get; set; }


        public double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}