using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DowningLLP;
using System.Configuration;
using Test.Framework;
using Test.Framework.Helpers;
using Test.Framework.Reporting;
using Selenium.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Remote;
using MSTestHacks;
using DowningLLP.ExcelDataProvider;
using DowningLLP.PageObjects;

namespace DowningLLP.TestCases
{
    [TestClass]
    public class RegistrationTest : TestBase
    {
        private string url = string.Empty;
        ExcelDataAccess excelDataAccess = new ExcelDataAccess();
        UserData alreadyRegisteredUserData = new UserData();



        [TestInitialize]
        public void BasicSetUp()
        {

            alreadyRegisteredUserData = excelDataAccess.GetTestData("RegisteredUser");
            string urlXML = ConfigurationManager.AppSettings["URLMapping"];
            string url = CommonFunctions.GetXMLNode(urlXML, "Downing");
            Browser.Start(url);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            TestCaseResult result = new TestCaseResult();
            result.TestCaseName = this.TestContext.TestName;
            result.ResultDir = this.TestContext.ResultsDirectory;

            UnitTestOutcome executionResult = this.TestContext.CurrentTestOutcome;

            if (executionResult == UnitTestOutcome.Passed)
                result.ExecutionResult = TestExecutionOutcome.Pass;
            else
                result.ExecutionResult = TestExecutionOutcome.Fail;


            var currentlyRunningClassType = this.GetType().Assembly.GetTypes().FirstOrDefault(f => f.FullName == this.TestContext.FullyQualifiedTestClassName);
            var method = currentlyRunningClassType.GetMethod(this.TestContext.TestName);
            var workItemAttribute = method.GetCustomAttributes(typeof(TestCategoryAttribute)).First();

            result.TestCaseCategory = ((TestCategoryAttribute)workItemAttribute).TestCategories[0];

            resultSet.Add(result);

        }
        [TestMethod]
        [TestCategory("Registration")]
        public void Downing_Registration_Success()
        {
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegEnterEmail();
                },
                1,
                @"User entered Email ID", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegAboutUs();
                },
                2,
                @"User selected the How you hear about us dropdown", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegPleaseSpecify();
                },
                3,
                @"User entered the source", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegTerms();
                },
                4,
                @"User Accept Terms and Condition", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegComYes();
                },
                5,
                @"User selected communication preference", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegSubmit();
                },
                6,
                @"User submitted the registration form", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegSuccess();
                },
                7,
                @"User accept regressistration success pop-up", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningAccount();
                },
                8,
                @"User cliked on Account button", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.MyProfile();
                },
                9,
                @"User clicked on MyProfile button", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.MySelf();
                },
                10,
                @"User selected on Personal details investment section", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.MyTitle();
                },
                11,
                @"User selected Title", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningFirstName();
                },
                12,
                @"User entered Firstname", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningLastName();
                },
                13,
                @"User entered LastName", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DobDay();
                },
                14,
                @"User selected birth day", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DobMonth();
                },
                15,
                @"User selected birth month", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DobYear();
                },
                16,
                @"User selected birth year", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningPhone();
                },
                17,
                @"User entered phone number", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningIntermediary();
                },
                18,
                @"User entered intermediary details", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningNatInsurance();
                },
                19,
                @"User entered insurance number", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningPostCode(alreadyRegisteredUserData.PostCode);
                },
                20,
                @"User entered postcode ", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.PostCodeLook();
                },
                20,
                @"User entered postcode ", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningMoveInMonth();
                },
                21,
                @"User seelcted move in month ", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningMoveInYear();
                },
                22,
                @"User selected move in year ", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningSaveBtn();
                },
                23,
                @"User Saved the details ", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.ComPreNo();
                },
                24,
                @"User selected on Communication Preference", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.MyPassword(alreadyRegisteredUserData.Password);
                },
                25,
                @"User updated the password field", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.PassSave();
                },
                26,
                @"User updated the password", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.LogOut();
                },
                26,
                @"User logged out", ScreenCapture.Accept);



        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Downing_Email_Validation()
        {
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegEnterEmail();
                },
                1,
                @"User entered Email ID", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegAboutUs();
                },
                2,
                @"User selected the How you hear about us dropdown", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegPleaseSpecify();
                },
                3,
                @"User entered the source", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegTerms();
                },
                4,
                @"User Accept Terms and Condition", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegComYes();
                },
                5,
                @"User selected communication preference", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegSubmit();
                },
                6,
                @"User submitted the registration form", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.RegistrationPage.DowningRegSuccess();
                },
                7,
                @"User accept regressistration success pop-up", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.DowningAccount();
                },
                8,
                @"User cliked on Account button", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    PageObjects.MyAccountPage.MyProfile();
                },
                9,
                @"User clicked on MyProfile button", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    Browser.BringElementToView(BySelection.XPath, "RegEmailID");  
                },
                10,
                @"validating Reistered useer's email", ScreenCapture.Accept);
            TestStep.Execute(
                () =>
                {
                    MyAccountPage.DowningEmailValidation.Should().BeTrue();
                },
                11,
                @"Validating Email Txt"
                );

        }
    } 
}
