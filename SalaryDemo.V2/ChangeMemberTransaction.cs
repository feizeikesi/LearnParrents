namespace SalaryDemo.V2
{
    public class ChangeMemberTransaction:ChangeAffiliationTransaction
    {
        private readonly int _memberId;
        private readonly double _dues;
        public ChangeMemberTransaction(int empId,int memberId,  double dues)
            : base(empId)
        {
            _memberId = memberId;
            _dues = dues;
        }

        protected override IAffiliation Affiliation
        {
            get
            {
                
                return new UnionAffiliation();
            }
        }

        protected override void RecordMembership(Employee e)
        {
            PayrollDatabase.AddUnionMember(_memberId, e);
        }
    }
}