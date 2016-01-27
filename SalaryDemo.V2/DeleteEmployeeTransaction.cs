namespace SalaryDemo.V2
{
    public class DeleteEmployeeTransaction : ITransaction
    {
        private readonly int _id;
        public DeleteEmployeeTransaction(int empid)
        {
            _id = empid;
        }

        public int Execute()
        {
            PayrollDatabase.DeleteEmployee(_id);
            return 1;
        }
    }
}