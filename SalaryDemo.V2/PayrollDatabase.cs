using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalaryDemo.V2
{
    public class PayrollDatabase
    {
        private static readonly Hashtable employees = new Hashtable();
        private static readonly Dictionary<Employee, int> unionMembers = new Dictionary<Employee, int>();

        public static Employee GetEmployee(int empid)
        {
            return employees[empid] as Employee;
        }

        public static void AddEmployee(int empid, Employee employee)
        {
            employees[empid] = employee;
        }

        public static void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }

        public static Employee GetUnionMember(int memberId)
        {
            return unionMembers.FirstOrDefault(a => a.Value == memberId).Key;
        }

        public static void AddUnionMember(int memberId, Employee employee)
        {
            if (unionMembers.ContainsKey(employee))
            {
                unionMembers[employee] = memberId;
            }
            else
            {
                unionMembers.Add(employee, memberId);
            }
        }

        public static void RemoveUnionMember(int memberId)
        {
            var e = GetUnionMember(memberId);
            if (e != null)
            {
                unionMembers.Remove(e);
            }
        }

        public static IEnumerable<int> GetAllEmployeeIds()
        {
           return employees.Keys.OfType<int>();
        }
    }
}