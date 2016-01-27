namespace SalaryDemo.V2
{
    public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empid) : base(empid)
        {
        }

        protected override IAffiliation Affiliation
        {
            get { return new  NoAffiliation(); }
        }

        protected override void RecordMembership(Employee e)
        {
            IAffiliation affiliation = e.Affiliation;
            if (affiliation is UnionAffiliation)
            {
                UnionAffiliation unionAffiliation=affiliation as UnionAffiliation;
                int memberId = unionAffiliation.MemberId;
                PayrollDatabase.RemoveUnionMember(memberId);
            }
        }
    }
}