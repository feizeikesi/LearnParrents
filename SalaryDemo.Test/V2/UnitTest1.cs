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

            IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc=pc as SalariedClassification;

            Assert.AreEqual(1000.00,sc.Salary,.001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            IPayemntMethod pm = e.Method;
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

            IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification sc = pc as HourlyClassification;

            Assert.AreEqual(1000.00, sc.Salary, .001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            IPayemntMethod pm = e.Method;
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

            IPaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;

            Assert.AreEqual(1000.00, sc.Salary, .001);
            IPaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            IPayemntMethod pm = e.Method;
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

            IPaymentClassification pc = e.Classification;
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

            IPaymentClassification pc = e.Classification;
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
            PaydayTransaction pt=new PaydayTransaction(payDate);
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

            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        /// <summary>
        /// 支付钟点工薪水
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmplouyeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t=new AddHourlyEmployee(empId,"Bill","Home",15.25);
            t.Execute();
            DateTime payDate=new DateTime(2001,11,9);
            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();

        

            ValidateHourlyPaycheck(pt,empId,payDate,0.0);
        }

        private static void ValidateHourlyPaycheck(PaydayTransaction pt,int empId, DateTime payDate,double pay)
        {
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay, 0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, 0.001);
            Assert.AreEqual(pay, pc.NetPay, 0.001);
        }

        /// <summary>
        /// 是否添加考勤卡后就可以支付薪水
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            TimeCardTransaction tc=new TimeCardTransaction(payDate,2,empId);
            tc.Execute();
            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();
        }

        /// <summary>
        /// 是否可以对超出8小时的考勤卡支付加班工资
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);

            TimeCardTransaction tc=new TimeCardTransaction(payDate,9,empId);
            tc.Execute();
            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt,empId,payDate,(8+1.5)*15.25);

        }

        /// <summary>
        /// 如果不是周五,系统就不支付钟点工工资
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 8);

            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9, empId);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Assert.IsNull(pt);
        }

        /// <summary>
        /// 为多个考勤卡的雇员计算薪水
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);

            TimeCardTransaction tc=new TimeCardTransaction(payDate,2,empId);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(payDate.AddDays(-1), 5, empId);
            tc2.Execute();

            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();

            ValidateHourlyPaycheck(pt,empId,payDate,7*15.25);
        }

        /// <summary>
        /// 只为当前支付期内的考勤卡对雇员进行支付
        /// </summary>
        [TestMethod]
        public void TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            DateTime dateInPreviousPayPeriod=new DateTime(2001,11,2);

            TimeCardTransaction tc=new TimeCardTransaction(payDate,2,empId);
            tc.Execute();

            TimeCardTransaction tc2=new TimeCardTransaction(dateInPreviousPayPeriod,5,empId);
            tc2.Execute();

            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt,empId,payDate,2*15.25);
        }

        /// <summary>
        /// 工会会员支付薪水时扣除会费
        /// </summary>
        [TestMethod]
        public void TestSalariedUnionMemberDues()
        {

            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
            t.Execute();
            int memberId = 7734;

            ChangeMemberTransaction cmt=new ChangeMemberTransaction(empId,memberId,9.42);
            cmt.Execute();
            DateTime payDate=new DateTime(2001,11,30);

            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate,pc.PayDate);
            Assert.AreEqual(1000.0,pc.GrossPay,0.001);
            Assert.AreEqual("Hold",pc.GetField("Disposition"));
            Assert.AreEqual(0,pc.Deductions,0.001);
            Assert.AreEqual(1000-0, pc.NetPay, 0.001);
        }

        /// <summary>
        /// 确定服务费正确扣除
        /// </summary>
        [TestMethod]
        public void TestHourlyUnionMemberServiceCharge()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24);
            t.Execute();
            int memberId = 7734;

            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 30);

            ServiceChargeTransaction sct=new ServiceChargeTransaction(memberId,payDate,19.42);
            sct.Execute();

            PaydayTransaction pt=new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);

            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate,pc.PayPeriodEndDate);
            Assert.AreEqual(8*15.24,pc.GrossPay,0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42+19.42, pc.Deductions, 0.001);
            Assert.AreEqual((8*15.24) - (9.42+19.42), pc.NetPay, 0.001);
        }

        /// <summary>
        /// 确定当前支付期间外的服务费用没有被扣除
        /// </summary>
        [TestMethod]
        public void TestServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24);
            t.Execute();
            int memberId = 7734;

            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            DateTime earlyDate = new DateTime(2001, 11, 2);//上一个周五
            DateTime lastDate=new DateTime(2001,11,16);//下个周五


            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            sct.Execute();

            ServiceChargeTransaction sctEarly = new ServiceChargeTransaction(memberId, earlyDate, 100);
            sctEarly.Execute();
            ServiceChargeTransaction sctLast = new ServiceChargeTransaction(memberId, lastDate, 200);
            sctLast.Execute();

            TimeCardTransaction tct=new TimeCardTransaction(payDate,8,empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);

            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, 0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, 0.001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, 0.001);
        }
    }

}
