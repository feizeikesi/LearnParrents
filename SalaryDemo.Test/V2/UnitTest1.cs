using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryDemo.V2;

namespace SalaryDemo.Test.V2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddSalariedEmployee()
        {
            int empid = 1;
            AddEmployeeTransaction t = new AddSalariedEmployee(empid, "Bob", "Home", 1000.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(e.Name, "Bob");

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc=pc as SalariedClassification;

            Assert.AreEqual(1000.00,sc.Salary,.001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PayemntMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);

        }

        [TestMethod]
        public void TestAddHourlyEmployee()
        {
            int empid = 1;
            AddEmployeeTransaction t = new AddHourlyEmployee(empid, "Bob", "Home", 10.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(e.Name, "Bob");

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification sc = pc as HourlyClassification;

            Assert.AreEqual(1000.00, sc.Salary, .001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PayemntMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);

        }


        [TestMethod]
        public void TestAddCommissionedEmployee()
        {
            int empid = 1;
            AddEmployeeTransaction t = new AddCommissionedEmployee(empid, "Bob", "Home", 1000.00,50.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(e.Name, "Bob");

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;

            Assert.AreEqual(1000.00, sc.Salary, .001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PayemntMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);

        }


        [TestMethod]
        public void TestDeleteEmployee()
        {
            int empid = 4;
            AddEmployeeTransaction t = new AddCommissionedEmployee(empid, "Bill", "Home", 1000.00, 50.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(e.Name, "Bill");

            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empid);
            dt.Execute();

            e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNull(e);
        }

        //考勤卡
        [TestMethod]
        public void TestTimeCardTransaction()
        {
            int empid = 5;
            AddHourlyEmployee t=new AddHourlyEmployee(empid,"bill","home",15.25);
            t.Execute();

            TimeCardTransaction tct=new TimeCardTransaction(DateTime.Now.Date, 8,empid);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);

            HourlyClassification hc=pc as HourlyClassification;
            TimeCard tc = hc.GetTimeCard(DateTime.Now.Date);
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0,tc.Hours);
        }

        [TestMethod]
        public void TestAddServiceCharge()
        {
            int empid = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "bill", "home", 15.25);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNotNull(e);

            UnionAffiliation af=new UnionAffiliation();
            e.Affiliation = af;
            int memberId = 86;
            PayrollDatabase.AddUnionMember(memberId, e);

            ServiceChargeTransaction sct=new ServiceChargeTransaction(memberId,DateTime.Now.Date,12.95);
            sct.Execute();

            ServiceCharge sc = af.GetServiceCharge(DateTime.Now.Date);
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95,sc.Amount,.001);
        }

        [TestMethod]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t=new AddHourlyEmployee(empId,"","",12.25);

            t.Execute();
            ChangeNameTransaction cnt=new ChangeNameTransaction(empId,"Bob");
            cnt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);
            Assert.AreEqual("Bob",e.Name);
        }

        [TestMethod]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee employee=new AddCommissionedEmployee(empId,"Lance","Home",2500,3.2);
            employee.Execute();

           ChangeHourlyTransaction cht=new ChangeHourlyTransaction(empId,27.52);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
           Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);

            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc=pc as HourlyClassification;
            Assert.AreEqual(27.52,hc.Salary,0.001);

            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }

        [TestMethod]
        public void TestChangeUnionMember()
        {
            int empId = 8;
            AddHourlyEmployee employee = new AddHourlyEmployee(empId, "Lance", "Home", 15.25);
            employee.Execute();
            int memberId = 7743;
            ChangeMemberTransaction cmt=new ChangeMemberTransaction(empId,memberId,99.42);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            IAffiliation affiliation = e.Affiliation;
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation uf=affiliation as UnionAffiliation;
            
            Assert.AreEqual(99.42, uf.Dues,0.001);

            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(e,member);
        }



        [TestMethod]
        public void TestPaySingleSalariedEmployee()
        {
            //测试不重复支付薪水
            int empId = 1;
            AddSalariedEmployee t=new AddSalariedEmployee(empId,"Bob","Home",1000.00);
            t.Execute();
            DateTime payDate=new DateTime(2001,11,30);
            PaydatTransaction pt=new PaydatTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate,pc.PayDate);
            Assert.AreEqual(1000.00,pc.GrossPay,0.001);
            Assert.AreEqual("Hold",pc.GetField("Disposition"));
            Assert.AreEqual(0.0,pc.Deductions,0.001);
            Assert.AreEqual(1000.00,pc.NetPay,0.001);
        }


        [TestMethod]
        public void TestPaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 1;
            AddSalariedEmployee t=new AddSalariedEmployee(empId,"Bob","Home",1000.00);
            t.Execute();
            DateTime payDate=new DateTime(2001,11,29);

            PaydatTransaction pt=new PaydatTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

    }

}
