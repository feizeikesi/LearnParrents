namespace SalaryDemo.V2
{
    /// <summary>
    /// 雇员改变工会会员状态事务
    /// </summary>
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {
        public ChangeAffiliationTransaction(int empid) : base(empid)
        {
        }

        protected override void Change(Employee employee)
        {
            RecordMembership(employee);
            IAffiliation affiliation =Affiliation;
            employee.Affiliation = affiliation;
        }

        protected abstract IAffiliation Affiliation { get; }
        /// <summary>
        /// 确定当前雇员是否为工会会员
        /// </summary>
        /// <param name="e"></param>
        protected abstract void RecordMembership(Employee e);
    }
}