using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Test.Framework;
using Test.Framework.Extensions.MSTest;
using Test.Framework.Helpers;
using Test.Framework.Reporting;
using System.Reflection;
using System;
using DowningLLP;

namespace DowningLLP
{
    [TestClass]
    public class TestBase : BaseTest
    {
        public static List<TestCaseResult> resultSet = new List<TestCaseResult>();

        static TestBase()
        {
            // write your class initialize logc here.
            System.AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            // write clean up logic here.
            ReportHelper.GetConsolidatedReport(resultSet);
        }

        /// <summary>
        /// To start the report log
        /// </summary>
        [TestInitialize]
        public  void BaseTestInitialize()
        {
            TestStep.TestContext = this.TestContext;

            ReportingManager.SetTestContext(this.TestContext);
            ReportingManager.StartReportAndLog();

        }


        [TestCleanup]
        public virtual void BaseTestCleanUps()
        {

            if (this.TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                // result.ExecutionResult = TestExecutionOutcome.Fail;

                Summary.ReportEvent(
                    ReportEventStatus.Fail,
                    "Test is not successful",
                    "Test is not successful",
                    ScreenCapture.Accept);

            }

            if (this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                // result.ExecutionResult = TestExecutionOutcome.Pass;
                Summary.ReportEvent(
                    ReportEventStatus.Pass,
                    "Test successful",
                    "Test successful",
                    ScreenCapture.Accept);
            }



        }




        public class TestCaseResult
        {
            public string TestCaseName { get; set; }
            public TestExecutionOutcome ExecutionResult { get; set; }
            //public string ExecutionResult{get; set;}

            public string TestCaseCategory { get; set; }

            public string ResultDir { get; set; }

            public string link { get; set; }

            public string logFile { get; set; }

        }

        public enum TestExecutionOutcome
        {
            Pass,
            Fail
        }

    }
}

