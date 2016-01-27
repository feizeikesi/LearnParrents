namespace SalaryDemo.V2
{
    /// <summary>
    /// 变更雇员的支付方式
    /// </summary>
    public class ChangeMethodTransaction:ChangeEmployeeTransaction
    {
        public ChangeMethodTransaction(int empid) : base(empid)
        {
        }

        protected override void Change(Employee employee)
        {
            throw new System.NotImplementedException();
        }
    }
}