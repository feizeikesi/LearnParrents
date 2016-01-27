namespace SalaryDemo.V2
{
    /// <summary>
    /// 月薪
    /// </summary>
    public class SalariedClassification : PaymentClassification
    {
        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double Salary { get; set; }


    }
}